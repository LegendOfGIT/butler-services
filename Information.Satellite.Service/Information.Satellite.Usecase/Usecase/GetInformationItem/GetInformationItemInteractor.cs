using Information.Satellite.Repository.Interfaces;
using Information.Satellite.Usecase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

      return new GetInformationItemInteractorResponse
      {
        ReleaseDate = GetContentByParsingCommands(content, request.ContentParsingCommand),
        WebContent = content
      };
    }

    private string GetContentByParsingCommands(string content, IContentParsingCommand parsingCommand)
    {
      var document = CQ.Create(content);

      if (parsingCommand == null)
      {
        return document.Text();
      }

      var parseCommand = parsingCommand as ParseByCssSelectionCommand;
      content = document[parseCommand.Selector].RenderSelection();

      return GetContentByParsingCommands(content, parsingCommand.ContentParsingCommand);
    }
  }
}
