using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;

namespace MicroSungero.Common
{
  /// <summary>
  /// String that can be dynamically localized at different cultures.
  /// </summary>
  public sealed class LocalizedString
  {
    #region Constants

    /// <summary>
    /// Space character.
    /// </summary>
    private const char SPACE = ' ';

    #endregion

    #region Properties and fields

    /// <summary>
    /// Current culture which the string will be localized at.
    /// </summary>
    public CultureInfo Culture => this.culture ?? Thread.CurrentThread.CurrentUICulture;

    private readonly CultureInfo culture;

    /// <summary>
    /// Resource manager.
    /// </summary>
    private ResourceManager ResourceManager { get; set; }

    /// <summary>
    /// String resource key.
    /// </summary>
    public string ResourceKey { get; private set; }

    /// <summary>
    /// Formatted string.
    /// </summary>
    public string StringFormat { get; private set; }

    /// <summary>
    /// Parameters of formatted string.
    /// </summary>
    public object[] Parameters { get; private set; }

    /// <summary>
    /// Localized string parts if current string is compound.
    /// </summary>
    private readonly Collection<LocalizedString> stringParts = new Collection<LocalizedString>();

    #endregion

    #region Methods

    /// <summary>
    /// Append localized string at the end of current localized string.
    /// </summary>
    /// <param name="localizedString">Localized string that will be appended at the end.</param>
    /// <returns>Current localized string that is compund now.</returns>
    public LocalizedString Append(LocalizedString localizedString)
    {
      if (localizedString != null && !ReferenceEquals(this, localizedString))
        this.stringParts.Add(localizedString);

      return this;
    }

    /// <summary>
    /// Append localized string at the new line of current localized string.
    /// </summary>
    /// <param name="localizedString">Localized string that will be appended at the new line.</param>
    /// <returns>Current localized string that is compund now.</returns>
    public LocalizedString AppendLine(LocalizedString localizedString)
    {
      if (this.stringParts.Any())
        this.Append(new LocalizedString(Environment.NewLine));

      this.Append(localizedString);
      return this;
    }

    /// <summary>
    /// Append formatted string at the end of current localized string.
    /// </summary>
    /// <param name="format">Formatted string that will be appended at the end.</param>
    /// <param name="parameters">Parameters of appending formatted string.</param>
    /// <returns>Current localized string that is compund now.</returns>
    public LocalizedString AppendFormat(string format, params object[] parameters)
    {
      return this.Append(new LocalizedString(format, parameters));
    }

    /// <summary>
    /// Check if the string, localized at the current culture, is null or empty.
    /// </summary>
    /// <returns>
    /// True:
    /// * if the string with specified resource key does not exist at resources
    /// or
    /// * if the string with specified resource key exists at resources and it is null or empty.
    /// </returns>
    public bool IsNullOrEmpty()
    {
      return this.IsNullOrEmpty(this.Culture);
    }

    /// <summary>
    /// Check if the string, localized at the specified culture, is null or empty.
    /// </summary>
    /// <param name="cultureInfo">Culture which current string is localized at.</param>
    /// <returns>
    /// True:
    /// * if the string with specified resource key does not exist at resources
    /// or
    /// * if the string with specified resource key exists at resources and it is null or empty.
    /// </returns>
    public bool IsNullOrEmpty(CultureInfo cultureInfo)
    {
      if (cultureInfo == null)
        throw new ArgumentNullException(nameof(cultureInfo));

      if (string.IsNullOrWhiteSpace(this.ResourceKey) || this.ResourceManager == null)
        return true;

      var stringFormat = this.ResourceManager.GetString(this.ResourceKey, cultureInfo);
      if (stringFormat == null)
        return true;

      return string.IsNullOrEmpty(string.Format(stringFormat, this.GetLocalizedParameters(cultureInfo)));
    }

    /// <summary>
    /// Check if the string, localized at the current culture, is null or contains only whitespace characters.
    /// </summary>
    /// <returns>
    /// True:
    /// * if the string with specified resource key does not exist at resources
    /// or
    /// * if the string with specified resource key exists at resources and it is null or contains only whitespace characters.
    /// </returns>
    public bool IsNullOrWhiteSpace()
    {
      return this.IsNullOrWhiteSpace(this.Culture);
    }

    /// <summary>
    /// Check if the string, localized at the specified culture, is null or contains only whitespace characters.
    /// </summary>
    /// <param name="cultureInfo">Culture which current string is localized at.</param>
    /// <returns>
    /// True:
    /// * if the string with specified resource key does not exist at resources
    /// or
    /// * if the string with specified resource key exists at resources and it is null or contains only whitespace characters.
    /// </returns>
    public bool IsNullOrWhiteSpace(CultureInfo cultureInfo)
    {
      if (cultureInfo == null)
        throw new ArgumentNullException(nameof(cultureInfo));

      if (string.IsNullOrWhiteSpace(this.ResourceKey) || this.ResourceManager == null)
        return true;

      var stringFormat = this.ResourceManager.GetString(this.ResourceKey, cultureInfo);
      if (stringFormat == null)
        return true;

      return string.IsNullOrWhiteSpace(string.Format(stringFormat, this.GetLocalizedParameters(cultureInfo)));
    }

    /// <summary>
    /// Получить строку.
    /// </summary>
    /// <param name="cultureInfo">Необходимая культура строки.</param>
    /// <returns>Строка.</returns>
    public string ToString(CultureInfo cultureInfo)
    {
      if (cultureInfo == null)
        throw new ArgumentNullException(nameof(cultureInfo));

      var stringBuilder = new StringBuilder();

      var stringFormat = (this.ResourceManager != null && !string.IsNullOrEmpty(this.ResourceKey) ?
        this.ResourceManager.GetString(this.ResourceKey, cultureInfo) : this.StringFormat);

      if (stringFormat == null)
      {
        if (this.ResourceManager != null && !string.IsNullOrEmpty(this.ResourceKey))
          stringBuilder.Append(this.ResourceKey);
      }
      else
      {
        if (this.stringParts.Count > 0)
          stringFormat = stringFormat.TrimEnd(SPACE);

        try
        {
          if (this.Parameters != null && this.Parameters.Any())
            stringBuilder.AppendFormat(cultureInfo, stringFormat, this.GetLocalizedParameters(cultureInfo));
          else
            stringBuilder.Append(stringFormat);
        }
        catch (FormatException ex)
        {
          stringBuilder.Append(stringFormat + $" ('{ex.Message}')");
        }
      }

      if (this.stringParts.Count > 0)
      {
        var prevString = stringBuilder.ToString();
        foreach (var localizedString in this.stringParts)
        {
          var currentString = localizedString.ToString(cultureInfo).TrimEnd(SPACE);
          if (NeedJoinWithSpace(cultureInfo, prevString, currentString))
            stringBuilder.Append(SPACE);

          if (!string.IsNullOrEmpty(currentString))
          {
            stringBuilder.Append(currentString);
            prevString = currentString;
          }
        }
      }

      return stringBuilder.ToString();
    }

    /// <summary>
    /// Check if the two strings should be joined with a space when joining them into single string.
    /// </summary>
    /// <param name="cultureInfo">Current culture.</param>
    /// <param name="prevString">The first joining string.</param>
    /// <param name="currentString">The second joining string.</param>
    /// <returns>True if we should add space between strings joining into single string, else False.</returns>
    private static bool NeedJoinWithSpace(CultureInfo cultureInfo, string prevString, string currentString)
    {
      if (string.IsNullOrWhiteSpace(prevString) || string.IsNullOrWhiteSpace(currentString))
        return false;

      if (prevString.EndsWith(SPACE.ToString(), true, cultureInfo))
        return false;

      if (prevString.EndsWith(Environment.NewLine, true, cultureInfo))
        return false;

      if (currentString.StartsWith(Environment.NewLine, true, cultureInfo) || currentString.StartsWith(SPACE.ToString(), true, cultureInfo))
        return false;

      return true;
    }

    /// <summary>
    /// Localize formatted string parameters.
    /// </summary>
    /// <param name="cultureInfo">Culture which parameters should be localized at.</param>
    /// <returns></returns>
    private IEnumerable<object> GetLocalizedParameters(CultureInfo cultureInfo)
    {
      return this.Parameters
        .Select(p => p is LocalizedString localizedString ? localizedString.ToString(cultureInfo) : p)
        .ToArray();
    }

    #endregion

    #region Object

    public static implicit operator string (LocalizedString localizedString)
    {
      return localizedString?.ToString();
    }

    public static LocalizedString operator + (LocalizedString str1, LocalizedString str2)
    {
      return str1.Append(str2);
    }

    public override string ToString()
    {
      return this.ToString(this.Culture);
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Create localized string.
    /// </summary>
    /// <param name="resourceManager">Resource manager.</param>
    /// <param name="resourceKey">String resource key.</param>
    /// <param name="defaultCulture">Default culture which current string should be localized at.</param>
    /// <param name="parameters">Parameters of formatted string.</param>
    public LocalizedString(ResourceManager resourceManager, string resourceKey, CultureInfo defaultCulture, params object[] parameters)
    {
      if (resourceManager == null)
        throw new ArgumentNullException(nameof(resourceManager));

      if (string.IsNullOrEmpty(resourceKey))
        throw new ArgumentNullException(nameof(resourceKey));

      this.ResourceManager = resourceManager;
      this.ResourceKey = resourceKey;
      this.culture = defaultCulture;
      this.StringFormat = null;
      this.Parameters = parameters;
    }

    /// <summary>
    /// Create localized string.
    /// </summary>
    /// <param name="format">Formatted string.</param>
    /// <param name="parameters">Parameters of formatted string.</param>
    public LocalizedString(string format, params object[] parameters)
    {
      if (format == null && (parameters != null && parameters.Length > 0))
        throw new ArgumentNullException(nameof(format));

      this.ResourceManager = null;
      this.ResourceKey = null;
      this.StringFormat = format ?? string.Empty;
      this.Parameters = parameters;
    }

    /// <summary>
    /// Create localized string.
    /// </summary>
    /// <param name="resourceManager">Resource manager.</param>
    /// <param name="resourceKey">String resource key.</param>
    /// <param name="parameters">Parameters of formatted string.</param>
    public LocalizedString(ResourceManager resourceManager, string resourceKey, params object[] parameters)
      : this(resourceManager, resourceKey, null, parameters)
    {
    }

    /// <summary>
    /// Create empty localized string.
    /// </summary>
    public LocalizedString() : this(string.Empty) 
    {
    }

    #endregion
  }
}
