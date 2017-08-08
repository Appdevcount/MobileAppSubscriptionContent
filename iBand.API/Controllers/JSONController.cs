using iBand.BL.Implementations;
using iBand.BL.Interfaces;
using iBand.Models;
using iBand.Models.Outputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace iBand.API.Controllers
{
    public class JSONController : ApiController
    {
        // Fields
        private IServicesBL services = new ServicesBL();

        // Methods
        [HttpPost]
        public DTO<CountriesAndOperators> CheckForUpdates(iBand.Models.Input<Models.Inputs.CountriesAndOperators> obj)
        {
            return this.services.GetCountriesAndOperators(obj);
        }


        [HttpPost]
        public DTO<RecentRBTs> GetRecentRBTs(Input<Models.Inputs.RecentRBTs> obj)
        {
            return this.services.GetRecentRBTs(obj);
        }



        [HttpPost]
        public DTO<RBTCategories> GetCategoriesRBT(Input<Models.Inputs.RBTCategories> obj)
        {
            return this.services.GetCategoriesRBT(obj);
        }


        [HttpPost]
        public DTO<AlbumsForCategory> GetAlbumsForCategory(Input<Models.Inputs.AlbumsForCategory> obj)
        {
            return this.services.GetAlbumsForCategory(obj);

        }

        [HttpPost]
        public DTO<Models.Outputs.RBTsForAlbums> GetRBTsForAlbums(Input<Models.Inputs.RBTsForAlbums> obj)
        {
            return this.services.GetRBTsForAlbums(obj);
        }

        [HttpPost]
        public DTO<Models.Outputs.RBTArtists> GetArtists(Input<Models.Inputs.RBTArtists> obj)
        {
            return this.services.GetArtists(obj);
        }

        [HttpPost]
        public DTO<Models.Outputs.RBTAlbumsForArtists> GetAlbumsForArtist(Input<Models.Inputs.RBTAlbumsForArtists> obj)
        {
            return this.services.GetAlbumsForArtist(obj);
        }

        [HttpPost]
        public DTO<Models.Outputs.RecentServicesDTO> GetRecentServices(Input<Models.Inputs.RecentServices> obj)
        {
            return this.services.GetRecentServices(obj);
        }

        [HttpPost]
        public DTO<Models.Outputs.ServiceCategoriesDTO> GetServiceCategories(Input<Models.Inputs.ServiceCategories> obj)
        {
            return this.services.GetServiceCategories(obj);
        }

        [HttpPost]
        public DTO<Models.Outputs.ServicesDTO> GetAllServices(Input<Models.Inputs.Services> obj)
        {
            return this.services.GetAllServices(obj);
        }


        [HttpPost]
        public DTO<Models.Outputs.VotingShowsDTO> GetVotingShows(Input<Models.Inputs.VotingShows> obj)
        {
            return this.services.GetVotingShows(obj);
        }

        [HttpPost]
        public DTO<Models.Outputs.ChattingShowsDTO> GetChattingShows(Input<Models.Inputs.ChattingShows> obj)
        {
            return this.services.GetChattingShows(obj);
        }

        [HttpPost]
        public DTO<Models.Outputs.GetContestantsForShowDTO> GetContestantsForShow(Input<Models.Inputs.GetContestantsForShow> obj)
        {
            return this.services.GetContestantsForShow(obj);
        }
        [HttpPost]
        public DTO<Models.Outputs.SetFavouriteDTO> SetFavourite(Input<Models.Inputs.SetFavourite> obj)
        {
            return this.services.SetFavourite(obj);
        }
        [HttpPost]
        public DTO<Models.Outputs.GetFavouritesDTO> GetFavourite(Input<Models.Inputs.GetFavourites> obj)
        {
            return this.services.GetFavourite(obj);
        }


    }


}
