using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iBand.DAL;
using iBand.Models;

namespace iBand.BL.Interfaces
{
    public interface IServicesBL
    {
        // Methods
       // DTO<Models.Outputs.RBTArtists> GetArtistsRBT(Input<Models.Inputs.RBTArtists> obj);
        
        DTO<Models.Outputs.CountriesAndOperators> GetCountriesAndOperators(Input<Models.Inputs.CountriesAndOperators> obj);
        DTO<Models.Outputs.RecentRBTs> GetRecentRBTs(Input<Models.Inputs.RecentRBTs> obj);
        
        DTO<Models.Outputs.RBTCategories> GetCategoriesRBT(Input<Models.Inputs.RBTCategories> obj);
        DTO<Models.Outputs.AlbumsForCategory> GetAlbumsForCategory(Input<Models.Inputs.AlbumsForCategory> obj);
        
        DTO<Models.Outputs.RBTsForAlbums> GetRBTsForAlbums(Input<Models.Inputs.RBTsForAlbums> obj);
        
        DTO<Models.Outputs.RBTArtists> GetArtists(Input<Models.Inputs.RBTArtists> obj);
        DTO<Models.Outputs.RBTAlbumsForArtists> GetAlbumsForArtist(Input<Models.Inputs.RBTAlbumsForArtists> obj);
       

        //Content Services


        DTO<Models.Outputs.RecentServicesDTO> GetRecentServices(Input<Models.Inputs.RecentServices> obj);
        DTO<Models.Outputs.ServiceCategoriesDTO> GetServiceCategories(Input<Models.Inputs.ServiceCategories> obj);
        DTO<Models.Outputs.ServicesDTO> GetAllServices(Input<Models.Inputs.Services> obj);

        DTO<Models.Outputs.VotingShowsDTO> GetVotingShows(Input<Models.Inputs.VotingShows> obj);

        DTO<Models.Outputs.ChattingShowsDTO> GetChattingShows(Input<Models.Inputs.ChattingShows> obj);

        DTO<Models.Outputs.GetContestantsForShowDTO> GetContestantsForShow(Input<Models.Inputs.GetContestantsForShow> obj);

        DTO<Models.Outputs.SetFavouriteDTO> SetFavourite(Input<Models.Inputs.SetFavourite> obj);
        DTO<Models.Outputs.GetFavouritesDTO> GetFavourite(Input<Models.Inputs.GetFavourites> obj);



    }


}
