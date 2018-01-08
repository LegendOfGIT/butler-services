using Information.Satellite.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using CsQuery;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Information.Satellite.Usecase.GetInformationItem
{
  public class GetInformationItemInteractor
  {
    private IWebContentRepository webContentRepository;

    public GetInformationItemInteractor(IWebContentRepository webContentRepository)
    {
      this.webContentRepository = webContentRepository;
    }

    public GetInformationItemInteractorResponse Execute(GetInformationItemInteractorRequest request)
    {
      var content = 
        (this.webContentRepository.GetWebContentAsString(request.Uri) ?? string.Empty)
          .Replace(Environment.NewLine, string.Empty);

      var properties = new Dictionary<string, IEnumerable<string>>();
      if (request.ContentParsingCommands != null) { 
        foreach(var parsingCommand in request.ContentParsingCommands)
        {
          properties[parsingCommand.TargetPropertyId] = GetContentByParsingCommands(content, parsingCommand);
        }
      }

      return new GetInformationItemInteractorResponse
      {
        Properties = properties,
        WebContent = content
      };
    }

    private IEnumerable<string> GetContentByParsingCommands(
      string content, 
      ParseCommand parsingCommand,
      ParseCommand parentParsingCommand = null
    )
    {
      if (parsingCommand == null)
      {
        return GetValuesFromParentParsingCommand(content, parentParsingCommand);
      }

      content = GetContentFromParsingCommand(content, parsingCommand);

      return GetContentByParsingCommands(
        content, 
        parsingCommand.ContentParsingCommand,
        parsingCommand
      );
    }

    private IEnumerable<string> GetValuesFromParentParsingCommand(string content, ParseCommand parsingCommand)
    {
      switch (parsingCommand.Type)
      {
        case ParseCommandType.Css:
          return GetValuesFromParentCssParsingCommand(content, parsingCommand);
      }

      return new[] { content };
    }
    private IEnumerable<string> GetValuesFromParentCssParsingCommand(string content, ParseCommand parsingCommand)
    {
      var document = CQ.Create(content);
      var targetAttribute = parsingCommand.Attribute;
      return document.Select(
          part =>
            string.IsNullOrEmpty(targetAttribute) ? part.InnerText
            : part.Attributes[targetAttribute]
      );
    }

    private string GetContentFromParsingCommand(string content, ParseCommand parsingCommand)
    {
      switch (parsingCommand.Type)
      {
        case ParseCommandType.Css:
          return GetContentFromCssParsingCommand(content, parsingCommand);
        case ParseCommandType.RegEx:
          return GetContentFromRegExParsingCommand(content, parsingCommand);
      }

      return string.Empty;
    }
    private string GetContentFromCssParsingCommand(string content, ParseCommand parsingCommand)
    {
      return CQ.Create(content)[parsingCommand.Selector].RenderSelection();
    }
    private string GetContentFromRegExParsingCommand(string content, ParseCommand parsingCommand)
    {
      var match = Regex.Match(content, parsingCommand.Selector);

      var targetIndex = parsingCommand.TargetIndex ?? 1;
      if(match != null && match.Groups.Count > targetIndex) { 
        return match.Groups[targetIndex].Value;
      }

      return string.Empty;
    }
  }
}
