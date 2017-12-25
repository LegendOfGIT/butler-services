using Information.Satellite.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Information.Satellite.Usecase
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
      var content = this.webContentRepository.GetWebContentAsString(request.Uri);

      var groups = 
        content == null 
          ? default(IEnumerable<Group>) 
          : Regex.Match(content, "<span.*itemprop=\"name\">(.*)</span>").Groups.OfType<Group>();

      var title = groups == null ? null : groups.Skip(1).FirstOrDefault();
      return new GetInformationItemInteractorResponse
      {
        Title = title == null ? string.Empty : title.Value,
        WebContent = content
      };
    }
  }
}
