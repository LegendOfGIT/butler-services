using Information.Satellite.Repository.Interfaces;
using Information.Satellite.Usecase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
      var content = this.webContentRepository.GetWebContentAsString(request.Uri).Replace(Environment.NewLine, string.Empty);

      return new GetInformationItemInteractorResponse
      {
        ReleaseDate = GetContentByParsingCommands(content, request.ContentParsingCommands.First()),
        WebContent = content
      };
    }

    private string GetContentByParsingCommands(string content, IContentParsingCommand parsingCommand)
    {
      if (parsingCommand == null)
      {
        return content;
      }

      var parseByTagCommand = parsingCommand as ParseByTagCommand;
      var attributes = string.Join(" ", parseByTagCommand.Attributes.Select(entry => { return $"{entry.Key}=\"{entry.Value}\""; }));

      var groups =
  content == null
    ? default(IEnumerable<Group>)
    : Regex.Match(content, $"<{parseByTagCommand.TagName}.*{attributes}>(.*)</{parseByTagCommand.TagName}>(</{parseByTagCommand.TagName}>)?").Groups.OfType<Group>();

      content = groups == null ? string.Empty : groups.Skip(1).FirstOrDefault().Value;

      return GetContentByParsingCommands(content, parsingCommand.ContentParsingCommand);
    }
  }
}
