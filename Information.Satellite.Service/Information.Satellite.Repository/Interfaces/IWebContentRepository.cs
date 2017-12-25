using System;

namespace Information.Satellite.Repository.Interfaces
{
  public interface IWebContentRepository
  {
    string GetWebContentAsString(Uri uri);
  }
}
