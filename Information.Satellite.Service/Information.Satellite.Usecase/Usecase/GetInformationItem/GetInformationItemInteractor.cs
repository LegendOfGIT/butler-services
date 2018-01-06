using Information.Satellite.Repository.Interfaces;
using Information.Satellite.Usecase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using CsQuery;

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
      IContentParsingCommand parsingCommand,
      IContentParsingCommand parentParsingCommand = null
    )
    {
      var document = CQ.Create(content);

      if (parsingCommand == null)
      {
        var parentParseCommand = parentParsingCommand as ParseByCssSelectionCommand;
        var targetAttribute = parentParseCommand?.Attribute;
        return document.Select(
          part =>
            string.IsNullOrEmpty(targetAttribute) ? part.InnerText
            : part.Attributes[targetAttribute]
        );
      }

      var parseCommand = parsingCommand as ParseByCssSelectionCommand;
      content = document[parseCommand.Selector].RenderSelection();

      return GetContentByParsingCommands(
        content, 
        parsingCommand.ContentParsingCommand,
        parsingCommand
      );
    }
  }
}
