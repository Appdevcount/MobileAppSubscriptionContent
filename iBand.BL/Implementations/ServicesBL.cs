using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iBand.BL.Interfaces;
using iBand.Common;
using iBand.DAL;
using iBand.Models;
using iBand.Models.Outputs;

namespace iBand.BL.Implementations
{
    public class ServicesBL : IServicesBL
    {
        // Fields
        public iBandEntities db = new iBandEntities();

        // Methods
        #region Public Methods

        #region RANNAT
        public DTO<CountriesAndOperators> GetCountriesAndOperators(Input<Models.Inputs.CountriesAndOperators> obj)
        {
            DTO<CountriesAndOperators> dto = new DTO<CountriesAndOperators>();
            CountriesAndOperators operators = new CountriesAndOperators();
            dto.objname = "GetCountriesAndOperators";
            try
            {
                if ((string.IsNullOrEmpty(obj.param.company) || string.IsNullOrEmpty(obj.param.deviceid)) || string.IsNullOrEmpty(obj.param.password))
                {
                    dto.status = new Status(800);
                    return dto;
                }
                operators.countries = this.GetCountriesWithOperators();
                dto.response = operators;
                dto.status = new Status(0);
            }
            catch (Exception exception)
            {
                TraceLog.WriteToLog(exception.Message.ToString() + "Trace = " + exception.StackTrace.ToString(), exception);
                dto.status = new Status(1);
                dto.status.info1 = exception.Message;
                if (exception.Message.ToString().StartsWith("ERROR"))
                {
                    dto.status = new Status(Convert.ToInt32(exception.InnerException.Message));
                }
            }
            dto.response = operators;
            return dto;
        }
        public DTO<RecentRBTs> GetRecentRBTs(Input<Models.Inputs.RecentRBTs> obj)
        {
            DTO<RecentRBTs> dto = new DTO<RecentRBTs>();
            RecentRBTs ts = new RecentRBTs();
            dto.objname = "GetRecentRBTs";
            try
            {
                if ((string.IsNullOrEmpty(obj.input.operatorid) || string.IsNullOrEmpty(obj.param.company)) || (string.IsNullOrEmpty(obj.param.deviceid) || string.IsNullOrEmpty(obj.param.password)))
                {
                    dto.status = new Status(800);
                    return dto;
                }
                int lastID = string.IsNullOrEmpty(obj.input.lastid) ? 0 : Convert.ToInt32(obj.input.lastid);
                int count = string.IsNullOrEmpty(obj.input.count) ? 20 : Convert.ToInt32(obj.input.count);
                long userid = string.IsNullOrEmpty(obj.input.userid) ? 0 : Convert.ToInt64(obj.input.userid);
                ts.rbts = (lastID == 0) ? this.getRecentRBTs(Convert.ToInt32(obj.input.operatorid), count, userid) : this.getRecentRBTs(Convert.ToInt32(obj.input.operatorid), count, lastID, userid);
                dto.response = ts;
                dto.status = new Status(0);
            }
            catch (Exception exception)
            {
                TraceLog.WriteToLog(exception.Message.ToString() + "Trace = " + exception.StackTrace.ToString(), exception);
                dto.status = new Status(1);
                dto.status.info1 = exception.Message;
                if (exception.Message.ToString().StartsWith("ERROR"))
                {
                    dto.status = new Status(Convert.ToInt32(exception.InnerException.Message));
                }
            }
            dto.response = ts;
            return dto;
        }

        public DTO<RBTCategories> GetCategoriesRBT(Input<Models.Inputs.RBTCategories> obj)
        {
            DTO<RBTCategories> dto = new DTO<RBTCategories>();
            RBTCategories categories = new RBTCategories();
            dto.objname = "GetCategoriesRBT";
            try
            {
                if ((string.IsNullOrEmpty(obj.input.operatorid) || string.IsNullOrEmpty(obj.param.company)) || (string.IsNullOrEmpty(obj.param.deviceid) || string.IsNullOrEmpty(obj.param.password)))
                {
                    dto.status = new Status(800);
                    return dto;
                }
                categories.categories = this.getCategoriesForOperatorID(Convert.ToInt32(obj.input.operatorid));
                dto.status = new Status(0);
            }
            catch (Exception exception)
            {
                TraceLog.WriteToLog(exception.Message.ToString() + "Trace = " + exception.StackTrace.ToString(), exception);
                dto.status = new Status(1);
                dto.status.info1 = exception.Message;
                if (exception.Message.ToString().StartsWith("ERROR"))
                {
                    dto.status = new Status(Convert.ToInt32(exception.InnerException.Message));
                }
            }
            dto.response = categories;
            return dto;
        }
        public DTO<Models.Outputs.AlbumsForCategory> GetAlbumsForCategory(Input<Models.Inputs.AlbumsForCategory> obj)
        {
            DTO<Models.Outputs.AlbumsForCategory> dto = new DTO<AlbumsForCategory>();
            Models.Outputs.AlbumsForCategory ts = new Models.Outputs.AlbumsForCategory();
            dto.objname = "GetAlbumsForCategory";
            try
            {
                if ((string.IsNullOrEmpty(obj.input.operatorid) || string.IsNullOrEmpty(obj.param.company)) || (string.IsNullOrEmpty(obj.param.deviceid) || string.IsNullOrEmpty(obj.param.password)))
                {
                    dto.status = new Status(800);
                    return dto;
                }

                ts.albums = getAlbumsForCategoryID(Convert.ToInt32(obj.input.categoryid), Convert.ToInt32(obj.input.operatorid));
                dto.response = ts;
                dto.status = new Status(0);
            }
            catch (Exception exception)
            {
                TraceLog.WriteToLog(exception.Message.ToString() + "Trace = " + exception.StackTrace.ToString(), exception);
                dto.status = new Status(1);
                dto.status.info1 = exception.Message;
                if (exception.Message.ToString().StartsWith("ERROR"))
                {
                    dto.status = new Status(Convert.ToInt32(exception.InnerException.Message));
                }
            }
            dto.response = ts;
            return dto;
        }
        public DTO<Models.Outputs.RBTsForAlbums> GetRBTsForAlbums(Input<Models.Inputs.RBTsForAlbums> obj)
        {
            DTO<Models.Outputs.RBTsForAlbums> dto = new DTO<RBTsForAlbums>();
            Models.Outputs.RBTsForAlbums ts = new Models.Outputs.RBTsForAlbums();
            dto.objname = "GetRBTsForAlbums";
            try
            {
                if ((string.IsNullOrEmpty(obj.input.operatorid) || string.IsNullOrEmpty(obj.param.company)) || (string.IsNullOrEmpty(obj.param.deviceid) || string.IsNullOrEmpty(obj.param.password)))
                {
                    dto.status = new Status(800);
                    return dto;
                }
                int userid = string.IsNullOrEmpty(obj.input.userid) ? 0 : Convert.ToInt32(obj.input.userid);

                if (string.IsNullOrEmpty(obj.input.artistid))
                { ts.RBTs = getRBTsForAlbum(Convert.ToInt32(obj.input.operatorid), Convert.ToInt32(obj.input.albumid), userid); }
                else
                { ts.RBTs = getRBTsForAlbum(Convert.ToInt32(obj.input.operatorid), Convert.ToInt32(obj.input.albumid), Convert.ToInt32(obj.input.artistid), Convert.ToInt32(obj.input.userid)); }


                dto.response = ts;
                dto.status = new Status(0);
            }
            catch (Exception exception)
            {
                TraceLog.WriteToLog(exception.Message.ToString() + "Trace = " + exception.StackTrace.ToString(), exception);
                dto.status = new Status(1);
                dto.status.info1 = exception.Message;
                if (exception.Message.ToString().StartsWith("ERROR"))
                {
                    dto.status = new Status(Convert.ToInt32(exception.InnerException.Message));
                }
            }
            dto.response = ts;
            return dto;
        }

        public DTO<Models.Outputs.RBTArtists> GetArtists(Input<Models.Inputs.RBTArtists> obj)
        {
            DTO<RBTArtists> dto = new DTO<RBTArtists>();
            RBTArtists categories = new RBTArtists();
            dto.objname = "GetArtists";
            try
            {
                if ((string.IsNullOrEmpty(obj.input.operatorid) || string.IsNullOrEmpty(obj.param.company)) || (string.IsNullOrEmpty(obj.param.deviceid) || string.IsNullOrEmpty(obj.param.password)))
                {
                    dto.status = new Status(800);
                    return dto;
                }
                categories.artists = this.getArtistsForOperatorID(Convert.ToInt32(obj.input.operatorid));
                dto.status = new Status(0);
            }
            catch (Exception exception)
            {
                TraceLog.WriteToLog(exception.Message.ToString() + "Trace = " + exception.StackTrace.ToString(), exception);
                dto.status = new Status(1);
                dto.status.info1 = exception.Message;
                if (exception.Message.ToString().StartsWith("ERROR"))
                {
                    dto.status = new Status(Convert.ToInt32(exception.InnerException.Message));
                }
            }
            dto.response = categories;
            return dto;
        }
        public DTO<Models.Outputs.RBTAlbumsForArtists> GetAlbumsForArtist(Input<Models.Inputs.RBTAlbumsForArtists> obj)
        {
            DTO<Models.Outputs.RBTAlbumsForArtists> dto = new DTO<RBTAlbumsForArtists>();
            Models.Outputs.RBTAlbumsForArtists ts = new Models.Outputs.RBTAlbumsForArtists();
            dto.objname = "GetAlbumsForArtist";
            try
            {
                if ((string.IsNullOrEmpty(obj.input.operatorid) || string.IsNullOrEmpty(obj.param.company)) || (string.IsNullOrEmpty(obj.param.deviceid) || string.IsNullOrEmpty(obj.param.password)))
                {
                    dto.status = new Status(800);
                    return dto;
                }

                ts.albums = getAlbumsForArtistID(Convert.ToInt32(obj.input.artistid), Convert.ToInt32(obj.input.operatorid));
                dto.response = ts;
                dto.status = new Status(0);
            }
            catch (Exception exception)
            {
                TraceLog.WriteToLog(exception.Message.ToString() + "Trace = " + exception.StackTrace.ToString(), exception);
                dto.status = new Status(1);
                dto.status.info1 = exception.Message;
                if (exception.Message.ToString().StartsWith("ERROR"))
                {
                    dto.status = new Status(Convert.ToInt32(exception.InnerException.Message));
                }
            }
            dto.response = ts;
            return dto;
        }



        #endregion RANNAT

        #region CONTENT
        public DTO<Models.Outputs.RecentServicesDTO> GetRecentServices(Input<Models.Inputs.RecentServices> obj)
        {
            DTO<Models.Outputs.RecentServicesDTO> dto = new DTO<RecentServicesDTO>();
            Models.Outputs.RecentServicesDTO ts = new Models.Outputs.RecentServicesDTO();
            dto.objname = "GetAlbumsForArtist";
            try
            {
                if ((string.IsNullOrEmpty(obj.input.operatorid) || string.IsNullOrEmpty(obj.param.company)) || (string.IsNullOrEmpty(obj.param.deviceid) || string.IsNullOrEmpty(obj.param.password)))
                {
                    dto.status = new Status(800);
                    return dto;
                }
                int userid = string.IsNullOrEmpty(obj.input.userid) ? 0 : Convert.ToInt32(obj.input.userid);

                ts.Services = GetServices(Convert.ToInt32(obj.input.operatorid), 0, string.IsNullOrEmpty(obj.input.count) ? 20 : Convert.ToInt32(obj.input.count), Convert.ToInt32(obj.input.lastid),userid);

                //ts.albums = getAlbumsForArtistID(Convert.ToInt32(obj.input.artistid), Convert.ToInt32(obj.input.operatorid));
                dto.response = ts;
                dto.status = new Status(0);
            }
            catch (Exception exception)
            {
                TraceLog.WriteToLog(exception.Message.ToString() + "Trace = " + exception.StackTrace.ToString(), exception);
                dto.status = new Status(1);
                dto.status.info1 = exception.Message;
                if (exception.Message.ToString().StartsWith("ERROR"))
                {
                    dto.status = new Status(Convert.ToInt32(exception.InnerException.Message));
                }
            }
            dto.response = ts;
            return dto;
        }
        public DTO<Models.Outputs.ServiceCategoriesDTO> GetServiceCategories(Input<Models.Inputs.ServiceCategories> obj)
        {
            DTO<Models.Outputs.ServiceCategoriesDTO> dto = new DTO<ServiceCategoriesDTO>();
            Models.Outputs.ServiceCategoriesDTO ts = new Models.Outputs.ServiceCategoriesDTO();
            dto.objname = "GetServiceCategories";
            try
            {
                if ((string.IsNullOrEmpty(obj.input.operatorid) || string.IsNullOrEmpty(obj.param.company)) || (string.IsNullOrEmpty(obj.param.deviceid) || string.IsNullOrEmpty(obj.param.password)))
                {
                    dto.status = new Status(800);
                    return dto;
                }

                ts.categories = GetServiceCategories(Convert.ToInt32(obj.input.operatorid));

                //ts.albums = getAlbumsForArtistID(Convert.ToInt32(obj.input.artistid), Convert.ToInt32(obj.input.operatorid));
                dto.response = ts;
                dto.status = new Status(0);
            }
            catch (Exception exception)
            {
                TraceLog.WriteToLog(exception.Message.ToString() + "Trace = " + exception.StackTrace.ToString(), exception);
                dto.status = new Status(1);
                dto.status.info1 = exception.Message;
                if (exception.Message.ToString().StartsWith("ERROR"))
                {
                    dto.status = new Status(Convert.ToInt32(exception.InnerException.Message));
                }
            }
            dto.response = ts;
            return dto;
        }
        public DTO<Models.Outputs.ServicesDTO> GetAllServices(Input<Models.Inputs.Services> obj)
        {
            DTO<Models.Outputs.ServicesDTO> dto = new DTO<ServicesDTO>();
            Models.Outputs.ServicesDTO ts = new Models.Outputs.ServicesDTO();
            dto.objname = "GetAllServices";
            try
            {
                if ((string.IsNullOrEmpty(obj.input.operatorid) || string.IsNullOrEmpty(obj.param.company)) || (string.IsNullOrEmpty(obj.param.deviceid) || string.IsNullOrEmpty(obj.param.password)))
                {
                    dto.status = new Status(800);
                    return dto;
                }
                int userid = string.IsNullOrEmpty(obj.input.userid) ? 0 : Convert.ToInt32(obj.input.userid);
                ts.Services = GetServices(Convert.ToInt32(obj.input.operatorid), string.IsNullOrEmpty(obj.input.categoryid) ? 0 : Convert.ToInt32(obj.input.categoryid), string.IsNullOrEmpty(obj.input.count) ? 20 : Convert.ToInt32(obj.input.count), Convert.ToInt32(obj.input.lastid),userid);
                dto.response = ts;
                dto.status = new Status(0);
            }
            catch (Exception exception)
            {
                TraceLog.WriteToLog(exception.Message.ToString() + "Trace = " + exception.StackTrace.ToString(), exception);
                dto.status = new Status(1);
                dto.status.info1 = exception.Message;
                if (exception.Message.ToString().StartsWith("ERROR"))
                {
                    dto.status = new Status(Convert.ToInt32(exception.InnerException.Message));
                }
            }
            dto.response = ts;
            return dto;
        }

        #endregion CONTENT

        #region INTERACTIVE
        public DTO<Models.Outputs.VotingShowsDTO> GetVotingShows(Input<Models.Inputs.VotingShows> obj)
        {
            DTO<Models.Outputs.VotingShowsDTO> dto = new DTO<VotingShowsDTO>();
            Models.Outputs.VotingShowsDTO ts = new Models.Outputs.VotingShowsDTO();
            dto.objname = "GetVotingShows";
            try
            {
                if ((string.IsNullOrEmpty(obj.input.operatorid) || string.IsNullOrEmpty(obj.param.company)) || (string.IsNullOrEmpty(obj.param.deviceid) || string.IsNullOrEmpty(obj.param.password)))
                {
                    dto.status = new Status(800);
                    return dto;
                }


                ts.shows = GetShows(Convert.ToInt32(obj.input.operatorid),Convert.ToInt32(obj.input.countryid), "VOTING");
                dto.response = ts;
                dto.status = new Status(0);
            }
            catch (Exception exception)
            {
                TraceLog.WriteToLog(exception.Message.ToString() + "Trace = " + exception.StackTrace.ToString(), exception);
                dto.status = new Status(1);
                dto.status.info1 = exception.Message;
                if (exception.Message.ToString().StartsWith("ERROR"))
                {
                    dto.status = new Status(Convert.ToInt32(exception.InnerException.Message));
                }
            }
            dto.response = ts;
            return dto;
        }

        public DTO<Models.Outputs.ChattingShowsDTO> GetChattingShows(Input<Models.Inputs.ChattingShows> obj)
        {
            DTO<Models.Outputs.ChattingShowsDTO> dto = new DTO<ChattingShowsDTO>();
            Models.Outputs.ChattingShowsDTO ts = new Models.Outputs.ChattingShowsDTO();
            dto.objname = "GetChattingShows";
            try
            {
                if ((string.IsNullOrEmpty(obj.input.operatorid) || string.IsNullOrEmpty(obj.param.company)) || (string.IsNullOrEmpty(obj.param.deviceid) || string.IsNullOrEmpty(obj.param.password)))
                {
                    dto.status = new Status(800);
                    return dto;
                }

                ts.shows = GetShows(Convert.ToInt32(obj.input.operatorid),Convert.ToInt32(obj.input.countryid), "CHATTING");
                dto.response = ts;
                dto.status = new Status(0);
            }
            catch (Exception exception)
            {
                TraceLog.WriteToLog(exception.Message.ToString() + "Trace = " + exception.StackTrace.ToString(), exception);
                dto.status = new Status(1);
                dto.status.info1 = exception.Message;
                if (exception.Message.ToString().StartsWith("ERROR"))
                {
                    dto.status = new Status(Convert.ToInt32(exception.InnerException.Message));
                }
            }
            dto.response = ts;
            return dto;
        }

        public DTO<Models.Outputs.GetContestantsForShowDTO> GetContestantsForShow(Input<Models.Inputs.GetContestantsForShow> obj)
        {
            DTO<Models.Outputs.GetContestantsForShowDTO> dto = new DTO<GetContestantsForShowDTO>();
            Models.Outputs.GetContestantsForShowDTO ts = new Models.Outputs.GetContestantsForShowDTO();
            dto.objname = "GetContestantsForShow";
            try
            {
                if ((string.IsNullOrEmpty(obj.param.company)) || (string.IsNullOrEmpty(obj.param.deviceid) || string.IsNullOrEmpty(obj.param.password)))
                {
                    dto.status = new Status(800);
                    return dto;
                }

                ts.Contestants = GetContestantsForShowID(Convert.ToInt32(obj.input.countryid),Convert.ToInt32(obj.input.operatorid), Convert.ToInt32(obj.input.showid));
                dto.response = ts;
                dto.status = new Status(0);
            }
            catch (Exception exception)
            {
                TraceLog.WriteToLog(exception.Message.ToString() + "Trace = " + exception.StackTrace.ToString(), exception);
                dto.status = new Status(1);
                dto.status.info1 = exception.Message;
                if (exception.Message.ToString().StartsWith("ERROR"))
                {
                    dto.status = new Status(Convert.ToInt32(exception.InnerException.Message));
                }
            }
            dto.response = ts;
            return dto;
        }
        #endregion INTERACTIVE

        public DTO<Models.Outputs.SetFavouriteDTO> SetFavourite(Input<Models.Inputs.SetFavourite> obj)
        {
            DTO<Models.Outputs.SetFavouriteDTO> dto = new DTO<SetFavouriteDTO>();
            Models.Outputs.SetFavouriteDTO ts = new Models.Outputs.SetFavouriteDTO();
            dto.objname = "SetFavourite";
            try
            {
                if ((string.IsNullOrEmpty(obj.param.company)) || (string.IsNullOrEmpty(obj.param.deviceid) || string.IsNullOrEmpty(obj.param.password)) || obj.input.ContentID == 0)
                {
                    dto.status = new Status(800);
                    return dto;
                }
                // ts.Contestants = GetContestantsForShowID(Convert.ToInt32(obj.input.operatorid), Convert.ToInt32(obj.input.showid));
                dto.response = ts;
                dto.status = new Status(0);
            }
            catch (Exception exception)
            {
                TraceLog.WriteToLog(exception.Message.ToString() + "Trace = " + exception.StackTrace.ToString(), exception);
                dto.status = new Status(1);
                dto.status.info1 = exception.Message;
                if (exception.Message.ToString().StartsWith("ERROR"))
                {
                    dto.status = new Status(Convert.ToInt32(exception.InnerException.Message));
                }
            }
            dto.response = ts;
            return dto;
        }
        public DTO<Models.Outputs.GetFavouritesDTO> GetFavourite(Input<Models.Inputs.GetFavourites> obj)
        {
            DTO<Models.Outputs.GetFavouritesDTO> dto = new DTO<GetFavouritesDTO>();
            Models.Outputs.GetFavouritesDTO ts = new Models.Outputs.GetFavouritesDTO();
            dto.objname = "GetFavourite";
            try
            {
                if ((string.IsNullOrEmpty(obj.param.company)) || (string.IsNullOrEmpty(obj.param.deviceid) || string.IsNullOrEmpty(obj.param.password)))
                {
                    dto.status = new Status(800);
                    return dto;
                }

                //ts.Contestants = GetContestantsForShowID(Convert.ToInt32(obj.input.operatorid), Convert.ToInt32(obj.input.showid));
                dto.response = ts;
                dto.status = new Status(0);
            }
            catch (Exception exception)
            {
                TraceLog.WriteToLog(exception.Message.ToString() + "Trace = " + exception.StackTrace.ToString(), exception);
                dto.status = new Status(1);
                dto.status.info1 = exception.Message;
                if (exception.Message.ToString().StartsWith("ERROR"))
                {
                    dto.status = new Status(Convert.ToInt32(exception.InnerException.Message));
                }
            }
            dto.response = ts;
            return dto;
        }

        #endregion Public Methods


        #region Private Methods

        public void setFavourite(int userid, int contentid, string contenttype)
        {
            var row = db.UserFavourites.Where(x => x.UserID == userid && x.ContentID == contentid && x.Type.Equals(contenttype)).FirstOrDefault();

            if (row == null)
            {
                UserFavourite fav = new UserFavourite();
                fav.UserID = userid;
                fav.Type = contenttype;
                fav.Status = true;
                fav.CreatedDate = DateTime.Now;
                fav.ContentID = contentid;

                db.UserFavourites.Add(fav);
                db.SaveChanges();
            }
            else
            {
                if (row.Status == true)
                    row.Status = false;
                else
                    row.Status = true;

                db.SaveChanges();

            }
        }

        public List<Favourite> GetFavouritesOfUser(int userid, int operatorid)
        {
            var rows = (from uf in db.UserFavourites
                        join
                        rbt in db.RBTs
                        on uf.ContentID equals rbt.ID
                        join
                        tn in db.Tones
                        on rbt.ToneID equals tn.ID
                        where uf.Status == true && uf.Type.Equals("rbt")
                        && tn.Status == true
                        && rbt.Status == true && uf.UserID == userid
                        orderby uf.ID descending
                        select new { rbt = rbt, uf = uf, tn = tn }).AsEnumerable().Select(x => new Favourite
                        {
                            contenttype = "rbt",
                            rbt = new Models.Outputs.RBT
                            {

                                price = (x.rbt.Price == null) ? "" : x.rbt.Price,
                                productid = (x.rbt.ProductID == null) ? "" : x.rbt.ProductID,
                                rbtdesc = (x.rbt.RBTDesc == null) ? "" : x.rbt.RBTDesc,
                                rbtname = (x.rbt.RBTName == null) ? "" : x.rbt.RBTName,
                                shortcode = (x.rbt.ShortCode == null) ? "" : x.rbt.ShortCode,
                                usermsg = (x.rbt.UserMsg == null) ? "" : x.rbt.UserMsg,
                                validityperiod = (!x.rbt.ValidityPeriod.HasValue ? 0 : x.rbt.ValidityPeriod).ToString(),
                                translations = this.getTranslations(new List<string> { x.rbt.RBTName, x.rbt.RBTDesc, x.rbt.UserMsg }),
                                tone = new Models.Outputs.tone
                                {
                                    imageurl = (x.tn.ImageURL == null) ? "" : x.tn.ImageURL,
                                    toneid = x.tn.ID.ToString(),
                                    tonename = (x.tn.ToneName == null) ? "" : x.tn.ToneName,
                                    tonedesc = (x.tn.ToneDescription == null) ? "" : x.tn.ToneDescription,
                                    translations = this.getTranslations(new List<string> { (x.tn.ToneName == null) ? "" : x.tn.ToneName, (x.tn.ToneDescription == null) ? "" : x.tn.ToneDescription })
                                }
                            }
                        }).ToList();

            var rows1 = (from uf in db.UserFavourites
                         join
                         srv in db.Services
                         on uf.ContentID equals srv.ID
                         join
                         sc in db.ServicesConfigs
                         on srv.ID equals sc.ServiceID
                         where uf.Status == true && uf.Type.Equals("service") &&
                         sc.Status == true && sc.OperatorID == operatorid
                         && srv.Status == true && uf.UserID == userid
                         orderby uf.ID descending
                         select new { srv = srv, uf = uf, sc = sc }).AsEnumerable().Select(x => new Favourite
                         {
                             contenttype = "service",
                             service = new Models.Outputs.service
                             {
                                 ImageURL = x.sc.ImageURL == null ? "" : x.sc.ImageURL,
                                 ImageURL2 = x.sc.ImageURL == null ? "" : x.sc.ImageURL,
                                 price = x.sc.Price == null ? "" : x.sc.Price.ToString(),
                                 productid = x.sc.ProductID == null ? "" : x.sc.ProductID.ToString(),
                                 servicedesc = string.IsNullOrEmpty(x.sc.ServiceDesc) ? (x.sc.ServiceDesc == null ? "" : x.sc.ServiceDesc) : x.sc.ServiceDesc,
                                 serviceid = x.sc.ID.ToString(),
                                 servicename = string.IsNullOrEmpty(x.sc.ServiceName) ? (x.sc.ServiceName == null ? "" : x.sc.ServiceName) : x.sc.ServiceName,
                                 shortcode = x.sc.ShortCode == null ? "" : x.sc.ShortCode,
                                 translations = getTranslations(new List<string>() { string.IsNullOrEmpty(x.sc.ServiceName) ? (x.sc.ServiceName == null ? "" : x.sc.ServiceName) : x.sc.ServiceName, string.IsNullOrEmpty(x.sc.ServiceDesc) ? (x.sc.ServiceDesc == null ? "" : x.sc.ServiceDesc) : x.sc.ServiceDesc }),
                                 usermsg = x.sc.UserMsg == null ? "" : x.sc.UserMsg,
                                 validityperiod = x.sc.VaidityPeriod.ToString()
                             }
                         }).ToList();


            List<Favourite> favs = new List<Favourite>();

            favs.AddRange(rows);
            favs.AddRange(rows1);

            return favs;



        }

        public List<tvshow> GetShows(int operatorid, int countryid, string type)
        {
            List<tvshow> shows = new List<tvshow>();

            shows = (from sho in db.InteractiveServices
                     join
                     scon in db.InteractiveServiceConfigs on sho.ID equals scon.InteractiveServiceID
                     join
                     op in db.Operators on scon.OperatorID equals op.ID
                     where sho.Status == true && scon.Status == true && op.Status == true && op.ID == operatorid
                     && scon.InteractiveServiceType.Equals(type)
                     select new { sho = sho, scon = scon }).OrderByDescending(x => x.sho.ID).AsEnumerable()
                         .Select(x => new tvshow
                         {
                             ImageURL = x.sho.ImageURL == null ? "" : x.sho.ImageURL,
                             ImageURL2 = x.sho.ImageURL2 == null ? "" : x.sho.ImageURL2,
                             price = x.scon.Price == null ? "0" : x.scon.Price.ToString(),
                             productid = x.scon.ProductID == null ? "" : x.scon.ProductID,
                             shortcode = x.scon.ShortCode == null ? "" : x.scon.ShortCode,
                             showdesc = string.IsNullOrEmpty(x.scon.Description) ? (string.IsNullOrEmpty(x.sho.ServiceDesc) ? "" : x.sho.ServiceDesc) : x.scon.Description,
                             showid = x.sho.ID.ToString(),
                             showname = string.IsNullOrEmpty(x.scon.Name) ? (string.IsNullOrEmpty(x.sho.InteractiveServiceName) ? "" : x.sho.InteractiveServiceName) : x.scon.Name,
                             paymentchannel = getPaymentChannels(countryid, x.sho.ID, operatorid, 2),
                             translations = getTranslations(new List<string>() { string.IsNullOrEmpty(x.scon.Name) ? (string.IsNullOrEmpty(x.sho.InteractiveServiceName) ? "" : x.sho.InteractiveServiceName) : x.scon.Name, string.IsNullOrEmpty(x.scon.Description) ? (string.IsNullOrEmpty(x.sho.ServiceDesc) ? "" : x.sho.ServiceDesc) : x.scon.Description }),
                             usermsg = x.scon.UserMsg == null ? "" : x.scon.UserMsg,
                             validityperiod = x.scon.ValidityPeriod.ToString()
                         }
                         ).ToList();


            return shows;
        }

        public List<contestant> GetContestantsForShowID(int countryid, int operatorid, int showid)
        {
            List<contestant> shows = new List<contestant>();

            shows = (from sho in db.InteractiveServices
                     join
                     scon in db.InteractiveServiceConfigs on sho.ID equals scon.InteractiveServiceID
                     join
                     op in db.Operators on scon.OperatorID equals op.ID
                     join
                     sc in db.InteractiveServiceContestants on sho.ID equals sc.InteractiveServiceID
                     join
                     co in db.Contestants on sc.ContestantID equals co.ID
                     where sho.Status == true && scon.Status == true && op.Status == true && op.ID == operatorid && sc.Status == true && sho.ID == showid && co.Status == true
                     select new { sc = sc, co = co }).OrderBy(x => x.sc.ContestantID).AsEnumerable()
                         .Select(x => new contestant
                         {
                             ImageURL = x.co.ImageURL == null ? "" : x.co.ImageURL,
                             contestantid = x.co.ID.ToString(),
                             contestantname = x.co.ContestantName,
                             contestantdesc = x.co.ContestantDescription == null ? "" : x.co.ContestantDescription,
                             paymentchannel = getPaymentChannels(countryid,showid,operatorid,2),
                             translations = getTranslations(new List<string>() { x.co.ContestantName, x.co.ContestantDescription == null ? "" : x.co.ContestantDescription }),
                             usermsg = x.sc.UserMsg == null ? "" : x.sc.UserMsg
                         }
                         ).ToList();

            return shows;
        }

        public List<service> GetServices(int operatorID, int categoryid, int count, int lastid,int userid)
        {
            List<service> services = new List<service>();

            if (categoryid == 0)
            {
                services = (from ser in db.Services
                            join uf in this.db.UserFavourites on ser.ID equals uf.ContentID into f
                            from isf in f.DefaultIfEmpty()
                            join
                                scon in db.ServicesConfigs on ser.ID equals scon.ServiceID
                            join
                              cat in db.Categories on scon.CategoryID equals cat.ID
                            join
                                op in db.Operators on scon.OperatorID equals op.ID
                            join
                                cou in db.Countries on op.CountryID equals cou.ID
                            where
                               ser.Status == true && ((isf.Type.Equals("service") && isf.UserID == userid) || isf.Type == null) && scon.Status == true && cat.Status == true
                               && op.Status == true && cou.Status == true && op.ID == operatorID
                             && ser.ID > lastid
                            select new //service
                            {
                                fav=isf,
                                serv = ser,
                                scon = scon
                            }

                        ).OrderByDescending(x => x.serv.ID).Take(count).AsEnumerable()
                        .Select(x => new service
                        {
                            ImageURL = x.serv.ImageURL == null ? "" : x.serv.ImageURL,
                            ImageURL2 = x.scon.ImageURL == null ? "" : x.scon.ImageURL,
                            price = x.scon.Price == null ? "" : x.scon.Price.ToString(),
                            productid = x.scon.ProductID == null ? "" : x.scon.ProductID.ToString(),
                            servicedesc = string.IsNullOrEmpty(x.scon.ServiceDesc) ? (x.serv.ServiceDesc == null ? "" : x.serv.ServiceDesc) : x.scon.ServiceDesc,
                            serviceid = x.serv.ID.ToString(),
                            servicename = string.IsNullOrEmpty(x.scon.ServiceName) ? (x.serv.ServiceName == null ? "" : x.serv.ServiceName) : x.scon.ServiceName,
                            shortcode = x.scon.ShortCode == null ? "" : x.scon.ShortCode,
                            translations = getTranslations(new List<string>() { string.IsNullOrEmpty(x.scon.ServiceName) ? (x.serv.ServiceName == null ? "" : x.serv.ServiceName) : x.scon.ServiceName, string.IsNullOrEmpty(x.scon.ServiceDesc) ? (x.serv.ServiceDesc == null ? "" : x.serv.ServiceDesc) : x.scon.ServiceDesc }),
                            isFavourite = (x.fav == null) ? "0" : ((x.fav.Status == false ? "0" : "1")),
                            usermsg = x.scon.UserMsg == null ? "" : x.scon.UserMsg,
                            validityperiod = x.scon.VaidityPeriod.ToString()
                        }
                        ).ToList();
            }
            else
            {
                services = (from ser in db.Services
                            join
                                scon in db.ServicesConfigs on ser.ID equals scon.ServiceID
                            join
                              cat in db.Categories on scon.CategoryID equals cat.ID
                            join
                                op in db.Operators on scon.OperatorID equals op.ID
                            join
                                cou in db.Countries on op.CountryID equals cou.ID
                            where
                               ser.Status == true && scon.Status == true && cat.Status == true
                               && op.Status == true && cou.Status == true && op.ID == operatorID
                               && cat.ID == categoryid
                                 && ser.ID > lastid
                            select new //service
                            {
                                serv = ser,
                                scon = scon
                            }
                        ).OrderByDescending(x => x.serv.ID).Take(count).AsEnumerable()
                        .Select(x => new service
                        {
                            ImageURL = x.serv.ImageURL == null ? "" : x.serv.ImageURL,
                            ImageURL2 = x.scon.ImageURL == null ? "" : x.scon.ImageURL,
                            price = x.scon.Price == null ? "" : x.scon.Price.ToString(),
                            productid = x.scon.ProductID == null ? "" : x.scon.ProductID.ToString(),
                            servicedesc = string.IsNullOrEmpty(x.scon.ServiceDesc) ? (x.serv.ServiceDesc == null ? "" : x.serv.ServiceDesc) : x.scon.ServiceDesc,
                            serviceid = x.serv.ID.ToString(),
                            servicename = string.IsNullOrEmpty(x.scon.ServiceName) ? (x.serv.ServiceName == null ? "" : x.serv.ServiceName) : x.scon.ServiceName,
                            shortcode = x.scon.ShortCode == null ? "" : x.scon.ShortCode,
                            translations = getTranslations(new List<string>() { string.IsNullOrEmpty(x.scon.ServiceName) ? (x.serv.ServiceName == null ? "" : x.serv.ServiceName) : x.scon.ServiceName, string.IsNullOrEmpty(x.scon.ServiceDesc) ? (x.serv.ServiceDesc == null ? "" : x.serv.ServiceDesc) : x.scon.ServiceDesc }),
                            usermsg = x.scon.UserMsg == null ? "" : x.scon.UserMsg,
                            validityperiod = x.scon.VaidityPeriod.ToString()
                        }
                        ).ToList();
            }


            return services;
        }

        public List<category> GetServiceCategories(int operatorID)
        {
            List<category> categories = new List<category>();


            categories = (from ser in db.Services
                          join
                              scon in db.ServicesConfigs on ser.ID equals scon.ServiceID
                          join
                            cat in db.Categories on scon.CategoryID equals cat.ID
                          join
                              op in db.Operators on scon.OperatorID equals op.ID
                          join
                              cou in db.Countries on op.CountryID equals cou.ID
                          where
                             ser.Status == true && scon.Status == true && cat.Status == true
                             && op.Status == true && cou.Status == true && op.ID == operatorID

                          group ser by cat into c
                          select new { Category = c.Key, AlbumCount = c.Count() }).OrderBy(x => x.Category.CategoryName).AsEnumerable()
                       .Select(x => new category
                       {
                           albumcount = x.AlbumCount.ToString(),
                           categoryname = x.Category.CategoryName,
                           categoryid = x.Category.ID.ToString(),
                           imageurl = x.Category.ImageURL == null ? "" : x.Category.ImageURL.ToString(),
                           translations = getTranslations(x.Category.CategoryName)
                       }
                       ).ToList();

            return categories;
        }

        private int AlbumCountForArtist(int artistid, int operatoid)
        {
            return 0;
        }

        private List<Models.Outputs.album> getAlbumsForCategoryID(int categoryid, int operatorid)
        {
            List<Models.Outputs.album> list = new List<Models.Outputs.album>();
            /*foreach (var type in (from <>h__TransparentIdentifier1 in from al in this.db.Albums
                join ton in this.db.Tones on al.ID equals ton.AlbumID into ton
                join rbt in this.db.RBTs on ton.ID equals rbt.ToneID into rbt
                where (((((al.Status == true) && (ton.Status == true)) && (rbt.Status == true)) && (al.CategoryID == categoryid)) && (ton.CategoryID == categoryid)) && (rbt.OperatorID == operatorid)
                select <>h__TransparentIdentifier1
                group <>h__TransparentIdentifier1.<>h__TransparentIdentifier0.al by <>h__TransparentIdentifier1.<>h__TransparentIdentifier0.al into algrp
                select new { al = algrp.Key, count = algrp.Count<Album>() }).ToList())
            {
                album album;
                album = new album {
                    albumid = type.al.ID.ToString(),
                    albumname = (type.al.AlbumName == null) ? "" : type.al.AlbumName,
                    imageurl = (type.al.ImageURL == null) ? "" : type.al.ImageURL,
                    rbtcount = type.count.ToString(),
                    translations = this.getTranslations(album.albumname)
                };
                list.Add(album);
            }*/


            var rows = (from cat in db.Categories
                        join
                            ton in db.Tones on cat.ID equals ton.CategoryID
                        join rbt in db.RBTs on ton.ID equals rbt.ToneID
                        join op in db.Operators on rbt.OperatorID equals op.ID
                        join al in db.Albums on cat.ID equals al.CategoryID
                        where
                        cat.Status == true && ton.Status == true
                        && rbt.Status == true && op.Status == true
                        && rbt.OperatorID == operatorid && al.Status == true
                        && cat.ID == categoryid
                        group rbt by al into c
                        select new { Album = c.Key, SongsCount = c.Count() }).OrderBy(x => x.Album.AlbumName).ToList();


            foreach (var row in rows)
            {
                album cat = new album();
                cat.rbtcount = row.SongsCount.ToString();
                cat.albumname = row.Album.AlbumName;
                cat.albumid = row.Album.ID.ToString();
                cat.imageurl = row.Album.ImageURL == null ? "" : row.Album.ImageURL.ToString();

                cat.translations = getTranslations(cat.albumname);
                list.Add(cat);


            }


            return list;
        }
        private List<Models.Outputs.album> getAlbumsForArtistID(int artistid, int operatorid)
        {
            List<Models.Outputs.album> list = new List<Models.Outputs.album>();
            /*foreach (var type in (from <>h__TransparentIdentifier1 in from al in this.db.Albums
                join ton in this.db.Tones on al.ID equals ton.AlbumID into ton
                join rbt in this.db.RBTs on ton.ID equals rbt.ToneID into rbt
                where (((((al.Status == true) && (ton.Status == true)) && (rbt.Status == true)) && (al.CategoryID == categoryid)) && (ton.CategoryID == categoryid)) && (rbt.OperatorID == operatorid)
                select <>h__TransparentIdentifier1
                group <>h__TransparentIdentifier1.<>h__TransparentIdentifier0.al by <>h__TransparentIdentifier1.<>h__TransparentIdentifier0.al into algrp
                select new { al = algrp.Key, count = algrp.Count<Album>() }).ToList())
            {
                album album;
                album = new album {
                    albumid = type.al.ID.ToString(),
                    albumname = (type.al.AlbumName == null) ? "" : type.al.AlbumName,
                    imageurl = (type.al.ImageURL == null) ? "" : type.al.ImageURL,
                    rbtcount = type.count.ToString(),
                    translations = this.getTranslations(album.albumname)
                };
                list.Add(album);
            }*/


            var rows = (from cat in db.Categories
                        join
                            ton in db.Tones on cat.ID equals ton.CategoryID
                        join rbt in db.RBTs on ton.ID equals rbt.ToneID
                        join op in db.Operators on rbt.OperatorID equals op.ID
                        join al in db.Albums on cat.ID equals al.CategoryID
                        join ar in db.Artists on ton.ArtistID equals ar.ID
                        where
                        cat.Status == true && ton.Status == true
                        && rbt.Status == true && op.Status == true
                        && rbt.OperatorID == operatorid && al.Status == true
                        && ar.ID == artistid
                        group rbt by al into c
                        select new { Album = c.Key, SongsCount = c.Count() }).OrderBy(x => x.Album.AlbumName).ToList();


            foreach (var row in rows)
            {
                album cat = new album();
                cat.rbtcount = row.SongsCount.ToString();
                cat.albumname = row.Album.AlbumName;
                cat.albumid = row.Album.ID.ToString();
                cat.imageurl = row.Album.ImageURL == null ? "" : row.Album.ImageURL.ToString();

                cat.translations = getTranslations(cat.albumname);
                list.Add(cat);


            }


            return list;
        }

        private List<Models.Outputs.artist> getArtistsForOperatorID(int operatorid)
        {
            List<Models.Outputs.artist> list = new List<Models.Outputs.artist>();


            //var rows = (from ar in this.db.Artists
            //      join ton in db.Tones on ar.ID equals ton.ArtistID 
            //      join rbt in this.db.RBTs on ton.ID equals rbt.ToneID 
            //      join al in this.db.Albums on ton.AlbumID equals (int?)al.ID 
            //      where (((rbt.OperatorID == operatorid) && (ar.Status == true)) && (ton.Status == true)) && (rbt.Status == true)
            //      select new { ar = ar, ton = ton, rbt = rbt, al = al }).ToList();


            //var rows1 =   (from x in
            //        (from ar in this.db.Artists
            //         join ton in db.Tones on ar.ID equals ton.ArtistID 
            //         join rbt in this.db.RBTs on ton.ID equals rbt.ToneID 
            //         join al in this.db.Albums on ton.AlbumID equals al.ID 

            //         where rbt.OperatorID == operatorid && ar.Status == true && ton.Status == true && rbt.Status == true
            //         select new { ar = ar, ton = ton, rbt = rbt, al = al }).ToList()
            //    group x by x.ar).ToList();



            var rows = (from ar in this.db.Artists
                        join ton in db.Tones on ar.ID equals ton.ArtistID
                        join rbt in this.db.RBTs on ton.ID equals rbt.ToneID
                        join al in this.db.Albums on ton.AlbumID equals al.ID
                        where rbt.OperatorID == operatorid && ar.Status == true && ton.Status == true && rbt.Status == true
                        group al by ar into c
                        select new { Artist = c.Key, AlbumCount = c.Count() }).OrderBy(x => x.Artist.ArtistName).ToList();


            foreach (var row in rows)
            {
                artist cat = new artist();
                cat.albumcount = row.AlbumCount.ToString();
                cat.artistname = row.Artist.ArtistName;
                cat.imageurl = row.Artist.ImageURL == null ? "" : row.Artist.ImageURL;
                cat.artistid = row.Artist.ID.ToString();

                cat.translations = getTranslations(cat.artistname);
                list.Add(cat);


            }


            //foreach (var row in rows1)
            //{
            //    Models.Outputs.artist ar = new artist();
            //    ar.artistid = row.Key.ID.ToString();
            //    ar.artistname = row.Key.ArtistName;
            //    ar.imageurl = row.Key.ImageURL == null ? "" : row.Key.ImageURL.ToString();
            //    ar.translations = getTranslations(ar.artistname);



            //   /* ar.albums = row

            //    cat.albumcount = row.AlbumCount.ToString();
            //    cat.categoryname = row.Category.CategoryName;
            //    cat.categoryid = row.Category.ID.ToString();
            //    cat.imageurl = row.Category.ImageURL == null ? "" : row.Category.ImageURL.ToString();
            //    cat.translations = getTranslations(cat.categoryname);*/
            //    list.Add(ar);


            //}



            return list;
        }

        public DTO<Models.Outputs.RBTArtists> GetArtistsRBT(Input<Models.Inputs.RBTArtists> obj)
        {
            DTO<RBTArtists> dto = new DTO<RBTArtists>();
            RBTArtists artists = new RBTArtists();
            dto.objname = "GetArtistsRBT";
            try
            {
                if ((string.IsNullOrEmpty(obj.input.operatorid) || string.IsNullOrEmpty(obj.param.company)) || (string.IsNullOrEmpty(obj.param.deviceid) || string.IsNullOrEmpty(obj.param.password)))
                {
                    dto.status = new Status(800);
                    return dto;
                }
                artists.artists = this.getArtistsForOperatorID(Convert.ToInt32(obj.input.operatorid));
                dto.status = new Status(0);
            }
            catch (Exception exception)
            {
                TraceLog.WriteToLog(exception.Message.ToString() + "Trace = " + exception.StackTrace.ToString(), exception);
                dto.status = new Status(1);
                dto.status.info1 = exception.Message;
                if (exception.Message.ToString().StartsWith("ERROR"))
                {
                    dto.status = new Status(Convert.ToInt32(exception.InnerException.Message));
                }
            }
            dto.response = artists;
            return dto;
        }

        private List<category> getCategoriesForOperatorID(int operatorid)
        {
            List<category> list = new List<category>();


            var rows = (from cat in db.Categories
                        join
                            ton in db.Tones on cat.ID equals ton.CategoryID
                        join rbt in db.RBTs on ton.ID equals rbt.ToneID
                        join op in db.Operators on rbt.OperatorID equals op.ID
                        join al in db.Albums on cat.ID equals al.CategoryID
                        where
                        cat.Status == true && ton.Status == true
                        && rbt.Status == true && op.Status == true
                        && rbt.OperatorID == operatorid && al.Status == true
                        group al by cat into c
                        select new { Category = c.Key, AlbumCount = c.Count() }).OrderBy(x => x.Category.CategoryName).ToList();


            foreach (var row in rows)
            {
                category cat = new category();
                cat.albumcount = row.AlbumCount.ToString();
                cat.categoryname = row.Category.CategoryName;
                cat.categoryid = row.Category.ID.ToString();
                cat.imageurl = row.Category.ImageURL == null ? "" : row.Category.ImageURL.ToString();
                cat.translations = getTranslations(cat.categoryname);
                list.Add(cat);


            }
            
            return list;
        }



        private List<Models.Outputs.Country> GetCountriesWithOperators()
        {
            List<Models.Outputs.Country> list = new List<Models.Outputs.Country>();

            list = (from country in this.db.Countries
                    where country.Status == true
                    orderby country.CountryName
                    select country).AsEnumerable()
                        .Select(country => new Models.Outputs.Country
                        {
                            countrycode = (country.CountryCode == null) ? "" : country.CountryCode,
                            countryid = country.ID.ToString(),
                            countryisd = (country.CountryISD == null) ? "" : country.CountryISD,
                            countryname = country.CountryName,
                            operators = this.GetOperatorsForCountryID(country.ID),
                            translations = this.getTranslations(country.CountryName)
                        }).ToList();



            return list;
        }

        private List<Models.Outputs.Operator> GetOperatorsForCountryID(int countryid)
        {
            List<Models.Outputs.Operator> list = new List<Models.Outputs.Operator>();


            list = (from op in this.db.Operators
                    where (op.Status == true) && (op.CountryID == countryid)
                    orderby op.OperatorName
                    select op).AsEnumerable()
                    .Select(op => new Models.Outputs.Operator
                    {
                        mcc = (op.MCC == null) ? "" : op.MCC,
                        mnc = (op.MNC == null) ? "" : op.MNC,
                        operatorid = op.ID.ToString(),
                        operatorname = (op.OperatorName == null) ? "" : op.OperatorName,
                        translations = this.getTranslations(op.OperatorName)

                    }).ToList();




            return list;
        }

        private List<Models.Outputs.RBT> getRecentRBTs(int operatorID, int count, long userid)
        {
            List<Models.Outputs.RBT> list = new List<Models.Outputs.RBT>();

            var rows = (from rbt in this.db.RBTs
                        join uf in this.db.UserFavourites on rbt.ID equals uf.ContentID into f
                        from isf in f.DefaultIfEmpty()
                        join ton in this.db.Tones on rbt.ToneID equals (int?)ton.ID
                        join al in this.db.Albums on ton.AlbumID equals (int?)al.ID
                        join cat in this.db.Categories on ton.CategoryID equals (int?)cat.ID
                        join art in this.db.Artists on ton.ArtistID equals (int?)art.ID
                        join com in this.db.Companies on ton.CompanyID equals (int?)com.ID
                        where (((((rbt.Status == true) && ((isf.Type.Equals("rbt") && isf.UserID == userid) || isf.Type == null) && rbt.OperatorID == operatorID && (ton.Status == true)) && (al.Status == true)) && (cat.Status == true)) && (art.Status == true)) && (com.Status == true)
                        select new { rbt1 = rbt, fav = isf, ton1 = ton, al = al, cat = cat, art = art, com = com }).ToList().Take(count);
            //new List<Models.Outputs.RBT>();

            foreach (var type in rows)
            {
                Models.Outputs.tone tone;
                Models.Outputs.album album;
                Models.Outputs.artist artist;
                Models.Outputs.category category;
                Models.Outputs.company company;


                Models.Outputs.RBT item = new Models.Outputs.RBT
                {
                    price = (type.rbt1.Price == null) ? "" : type.rbt1.Price,
                    productid = (type.rbt1.ProductID == null) ? "" : type.rbt1.ProductID,
                    rbtdesc = (type.rbt1.RBTDesc == null) ? "" : type.rbt1.RBTDesc,
                    rbtname = (type.rbt1.RBTName == null) ? "" : type.rbt1.RBTName,
                    shortcode = (type.rbt1.ShortCode == null) ? "" : type.rbt1.ShortCode,
                    usermsg = (type.rbt1.UserMsg == null) ? "" : type.rbt1.UserMsg,
                    isFavourite = (type.fav == null) ? "0" : ((type.fav.Status == false ? "0" : "1")),
                    rbtid = type.rbt1.ID.ToString()
                };
                item.validityperiod = (!type.rbt1.ValidityPeriod.HasValue ? 0 : type.rbt1.ValidityPeriod).ToString();
                item.translations = this.getTranslations(new List<string> { item.rbtname, item.rbtdesc, item.usermsg });
                tone = new Models.Outputs.tone
                {
                    imageurl = (type.ton1.ImageURL == null) ? "" : type.ton1.ImageURL,
                    toneid = type.ton1.ID.ToString(),
                    tonename = (type.ton1.ToneName == null) ? "" : type.ton1.ToneName,
                    tonedesc = (type.ton1.ToneDescription == null) ? "" : type.ton1.ToneDescription,
                    translations = this.getTranslations(new List<string> { (type.ton1.ToneName == null) ? "" : type.ton1.ToneName, (type.ton1.ToneDescription == null) ? "" : type.ton1.ToneDescription })
                };
                album = new Models.Outputs.album
                {
                    albumid = type.al.ID.ToString(),
                    albumname = (type.al.AlbumName == null) ? "" : type.al.AlbumName,
                    translations = this.getTranslations((type.al.AlbumName == null) ? "" : type.al.AlbumName),
                    imageurl = (type.al.ImageURL == null) ? "" : type.al.ImageURL
                };
                artist = new Models.Outputs.artist
                {
                    artistid = type.art.ID.ToString(),
                    artistname = type.art.ArtistName.ToString(),
                    imageurl = (type.art.ImageURL == null) ? "" : type.art.ImageURL,
                    translations = this.getTranslations(type.art.ArtistName.ToString())
                };
                category = new Models.Outputs.category
                {
                    categoryid = type.cat.ID.ToString(),
                    categoryname = (type.cat.CategoryName == null) ? "" : type.cat.CategoryName,
                    imageurl = (type.cat.ImageURL == null) ? "" : type.cat.ImageURL,
                    translations = this.getTranslations((type.cat.CategoryName == null) ? "" : type.cat.CategoryName)
                };
                company = new Models.Outputs.company
                {
                    companyid = type.com.ID.ToString(),
                    companyname = (type.com.CompanyName == null) ? "" : type.com.CompanyName,
                    imageurl = (type.com.ImageURL == null) ? "" : type.com.ImageURL,
                    translations = this.getTranslations((type.com.CompanyName == null) ? "" : type.com.CompanyName)
                };
                tone.album = album;
                tone.artist = artist;
                tone.category = category;
                tone.company = company;
                item.tone = tone;
                list.Add(item);
            }
            return list;
        }

        private List<Models.Outputs.RBT> getRecentRBTs(int operatorID, int count, int lastID, long userid)
        {
            List<Models.Outputs.RBT> list = new List<Models.Outputs.RBT>();
            var enumerable = (from rbt in this.db.RBTs
                              join uf in this.db.UserFavourites on rbt.ID equals uf.ContentID into f
                              from isf in f.DefaultIfEmpty()
                              join ton in this.db.Tones on rbt.ToneID equals (int?)ton.ID
                              join al in this.db.Albums on ton.AlbumID equals (int?)al.ID
                              join cat in this.db.Categories on ton.CategoryID equals (int?)cat.ID
                              join art in this.db.Artists on ton.ArtistID equals (int?)art.ID
                              join com in this.db.Companies on ton.CompanyID equals (int?)com.ID
                              where ((((((rbt.Status == true) && ((isf.Type.Equals("rbt") && isf.UserID == userid) || isf.Type == null) && rbt.OperatorID == operatorID && (ton.Status == true)) && (al.Status == true)) && (cat.Status == true)) && (art.Status == true)) && (com.Status == true)) && (rbt.ID >= lastID)
                              select new { rbt1 = rbt, fav = isf, ton1 = ton, al = al, cat = cat, art = art, com = com }).ToList().Take(count);
            new List<Models.Outputs.RBT>();

            foreach (var type in enumerable)
            {
                tone tone;
                album album;
                artist artist;
                category category;
                company company;
                Models.Outputs.RBT item = new Models.Outputs.RBT
                {
                    price = (type.rbt1.Price == null) ? "" : type.rbt1.Price,
                    productid = (type.rbt1.ProductID == null) ? "" : type.rbt1.ProductID,
                    rbtdesc = (type.rbt1.RBTDesc == null) ? "" : type.rbt1.RBTDesc,
                    rbtname = (type.rbt1.RBTName == null) ? "" : type.rbt1.RBTName,
                    shortcode = (type.rbt1.ShortCode == null) ? "" : type.rbt1.ShortCode,
                    usermsg = (type.rbt1.UserMsg == null) ? "" : type.rbt1.UserMsg,
                    isFavourite = (type.fav == null) ? "0" : ((type.fav.Status == false ? "0" : "1")),
                    rbtid = type.rbt1.ID.ToString()
                };
                item.validityperiod = (!type.rbt1.ValidityPeriod.HasValue ? 0 : type.rbt1.ValidityPeriod).ToString();
                item.translations = this.getTranslations(new List<string> { item.rbtname, item.rbtdesc, item.usermsg });
                tone = new tone
                {
                    imageurl = (type.ton1.ImageURL == null) ? "" : type.ton1.ImageURL,
                    toneid = type.ton1.ID.ToString(),
                    tonename = (type.ton1.ToneName == null) ? "" : type.ton1.ToneName,
                    tonedesc = (type.ton1.ToneDescription == null) ? "" : type.ton1.ToneDescription,
                    translations = this.getTranslations(new List<string> { (type.ton1.ToneName == null) ? "" : type.ton1.ToneName, (type.ton1.ToneDescription == null) ? "" : type.ton1.ToneDescription })
                };
                album = new album
                {
                    albumid = type.al.ID.ToString(),
                    albumname = (type.al.AlbumName == null) ? "" : type.al.AlbumName,
                    translations = this.getTranslations((type.al.AlbumName == null) ? "" : type.al.AlbumName),
                    imageurl = (type.al.ImageURL == null) ? "" : type.al.ImageURL
                };
                artist = new artist
                {
                    artistid = type.art.ID.ToString(),
                    artistname = type.art.ArtistName.ToString(),
                    imageurl = (type.art.ImageURL == null) ? "" : type.art.ImageURL,
                    translations = this.getTranslations(type.art.ArtistName)
                };
                category = new category
                {
                    categoryid = type.cat.ID.ToString(),
                    categoryname = (type.cat.CategoryName == null) ? "" : type.cat.CategoryName,
                    imageurl = (type.cat.ImageURL == null) ? "" : type.cat.ImageURL,
                    translations = this.getTranslations((type.cat.CategoryName == null) ? "" : type.cat.CategoryName)
                };
                company = new company
                {
                    companyid = type.com.ID.ToString(),
                    companyname = (type.com.CompanyName == null) ? "" : type.com.CompanyName,
                    imageurl = (type.com.ImageURL == null) ? "" : type.com.ImageURL,
                    translations = this.getTranslations((type.com.CompanyName == null) ? "" : type.com.CompanyName)
                };
                tone.album = album;
                tone.artist = artist;
                tone.category = category;
                tone.company = company;
                item.tone = tone;
                list.Add(item);
            }
            return list;
        }

        private List<Models.Outputs.RBT> getRBTsForAlbum(int operatorID, int albumid, int userid)
        {
            List<Models.Outputs.RBT> list = new List<Models.Outputs.RBT>();
            var rows = (from rbt in this.db.RBTs
                        join uf in this.db.UserFavourites on rbt.ID equals uf.ContentID into f
                        from isf in f.DefaultIfEmpty()
                        join ton in this.db.Tones on rbt.ToneID equals ton.ID
                        join al in this.db.Albums on ton.AlbumID equals al.ID
                        join cat in this.db.Categories on ton.CategoryID equals cat.ID
                        join art in this.db.Artists on ton.ArtistID equals art.ID
                        join com in this.db.Companies on ton.CompanyID equals com.ID
                        join op in db.Operators on rbt.OperatorID equals op.ID
                        where rbt.Status == true && ton.Status == true && al.Status == true && cat.Status == true
                        && art.Status == true && com.Status == true && op.ID == operatorID
                        && al.ID == albumid
                        select new { rbt1 = rbt, fav = isf, ton1 = ton, al = al, cat = cat, art = art, com = com }).ToList();
            // select rbt).ToList();

            // select new { rbt1 = rbt, ton1 = ton, al = al, cat = cat, art = art, com = com }).ToList();
            //new List<Models.Outputs.RBT>();

            foreach (var type in rows)
            {
                Models.Outputs.tone tone;
                Models.Outputs.album album;
                Models.Outputs.artist artist;
                Models.Outputs.category category;
                Models.Outputs.company company;


                Models.Outputs.RBT item = new Models.Outputs.RBT
                {
                    price = (type.rbt1.Price == null) ? "" : type.rbt1.Price,
                    productid = (type.rbt1.ProductID == null) ? "" : type.rbt1.ProductID,
                    rbtdesc = (type.rbt1.RBTDesc == null) ? "" : type.rbt1.RBTDesc,
                    rbtname = (type.rbt1.RBTName == null) ? "" : type.rbt1.RBTName,
                    shortcode = (type.rbt1.ShortCode == null) ? "" : type.rbt1.ShortCode,
                    usermsg = (type.rbt1.UserMsg == null) ? "" : type.rbt1.UserMsg,
                    isFavourite = (type.fav == null) ? "0" : ((type.fav.Status == false ? "0" : "1")),
                    rbtid = type.rbt1.ID.ToString()
                };
                item.validityperiod = (!type.rbt1.ValidityPeriod.HasValue ? 0 : type.rbt1.ValidityPeriod).ToString();
                item.translations = this.getTranslations(new List<string> { item.rbtname, item.rbtdesc, item.usermsg });
                tone = new Models.Outputs.tone
                {
                    imageurl = (type.ton1.ImageURL == null) ? "" : type.ton1.ImageURL,
                    toneid = type.ton1.ID.ToString(),
                    tonename = (type.ton1.ToneName == null) ? "" : type.ton1.ToneName,
                    tonedesc = (type.ton1.ToneDescription == null) ? "" : type.ton1.ToneDescription,
                    translations = this.getTranslations(new List<string> { (type.ton1.ToneName == null) ? "" : type.ton1.ToneName, (type.ton1.ToneDescription == null) ? "" : type.ton1.ToneDescription })
                };
                album = new Models.Outputs.album
                {
                    albumid = type.al.ID.ToString(),
                    albumname = (type.al.AlbumName == null) ? "" : type.al.AlbumName,
                    translations = this.getTranslations((type.al.AlbumName == null) ? "" : type.al.AlbumName),
                    imageurl = (type.al.ImageURL == null) ? "" : type.al.ImageURL
                };
                artist = new Models.Outputs.artist
                {
                    artistid = type.art.ID.ToString(),
                    artistname = type.art.ArtistName.ToString(),
                    imageurl = (type.art.ImageURL == null) ? "" : type.art.ImageURL,
                    translations = this.getTranslations(type.art.ArtistName.ToString())
                };
                category = new Models.Outputs.category
                {
                    categoryid = type.cat.ID.ToString(),
                    categoryname = (type.cat.CategoryName == null) ? "" : type.cat.CategoryName,
                    imageurl = (type.cat.ImageURL == null) ? "" : type.cat.ImageURL,
                    translations = this.getTranslations((type.cat.CategoryName == null) ? "" : type.cat.CategoryName)
                };
                company = new Models.Outputs.company
                {
                    companyid = type.com.ID.ToString(),
                    companyname = (type.com.CompanyName == null) ? "" : type.com.CompanyName,
                    imageurl = (type.com.ImageURL == null) ? "" : type.com.ImageURL,
                    translations = this.getTranslations((type.com.CompanyName == null) ? "" : type.com.CompanyName)
                };
                tone.album = album;
                tone.artist = artist;
                tone.category = category;
                tone.company = company;
                item.tone = tone;
                list.Add(item);
            }
            return list;
        }
        private List<Models.Outputs.RBT> getRBTsForAlbum(int operatorID, int albumid, int artistID, int userid)
        {
            List<Models.Outputs.RBT> list = new List<Models.Outputs.RBT>();
            var rows = (from rbt in this.db.RBTs
                        join uf in this.db.UserFavourites on rbt.ID equals uf.ContentID into f
                        from isf in f.DefaultIfEmpty()
                        join ton in this.db.Tones on rbt.ToneID equals (int?)ton.ID
                        join al in this.db.Albums on ton.AlbumID equals (int?)al.ID
                        join cat in this.db.Categories on ton.CategoryID equals (int?)cat.ID
                        join art in this.db.Artists on ton.ArtistID equals (int?)art.ID
                        join com in this.db.Companies on ton.CompanyID equals (int?)com.ID
                        join op in db.Operators on rbt.OperatorID equals op.ID
                        where rbt.Status == true && ton.Status == true && al.Status == true && cat.Status == true
                        && art.Status == true && com.Status == true
                        && al.ID == albumid && art.ID == artistID && op.ID == operatorID
                        select new { rbt1 = rbt, fav = isf, ton1 = ton, al = al, cat = cat, art = art, com = com }).ToList();
            //new List<Models.Outputs.RBT>();

            foreach (var type in rows)
            {
                Models.Outputs.tone tone;
                Models.Outputs.album album;
                Models.Outputs.artist artist;
                Models.Outputs.category category;
                Models.Outputs.company company;


                Models.Outputs.RBT item = new Models.Outputs.RBT
                {
                    price = (type.rbt1.Price == null) ? "" : type.rbt1.Price,
                    productid = (type.rbt1.ProductID == null) ? "" : type.rbt1.ProductID,
                    rbtdesc = (type.rbt1.RBTDesc == null) ? "" : type.rbt1.RBTDesc,
                    rbtname = (type.rbt1.RBTName == null) ? "" : type.rbt1.RBTName,
                    shortcode = (type.rbt1.ShortCode == null) ? "" : type.rbt1.ShortCode,
                    usermsg = (type.rbt1.UserMsg == null) ? "" : type.rbt1.UserMsg,
                    isFavourite = (type.fav == null) ? "0" : ((type.fav.Status == false ? "0" : "1")),
                    rbtid = type.rbt1.ID.ToString()
                };
                item.validityperiod = (!type.rbt1.ValidityPeriod.HasValue ? 0 : type.rbt1.ValidityPeriod).ToString();
                item.translations = this.getTranslations(new List<string> { item.rbtname, item.rbtdesc, item.usermsg });
                tone = new Models.Outputs.tone
                {
                    imageurl = (type.ton1.ImageURL == null) ? "" : type.ton1.ImageURL,
                    toneid = type.ton1.ID.ToString(),
                    tonename = (type.ton1.ToneName == null) ? "" : type.ton1.ToneName,
                    tonedesc = (type.ton1.ToneDescription == null) ? "" : type.ton1.ToneDescription,
                    translations = this.getTranslations(new List<string> { (type.ton1.ToneName == null) ? "" : type.ton1.ToneName, (type.ton1.ToneDescription == null) ? "" : type.ton1.ToneDescription })
                };
                album = new Models.Outputs.album
                {
                    albumid = type.al.ID.ToString(),
                    albumname = (type.al.AlbumName == null) ? "" : type.al.AlbumName,
                    translations = this.getTranslations((type.al.AlbumName == null) ? "" : type.al.AlbumName),
                    imageurl = (type.al.ImageURL == null) ? "" : type.al.ImageURL
                };
                artist = new Models.Outputs.artist
                {
                    artistid = type.art.ID.ToString(),
                    artistname = type.art.ArtistName.ToString(),
                    imageurl = (type.art.ImageURL == null) ? "" : type.art.ImageURL,
                    translations = this.getTranslations(type.art.ArtistName.ToString())
                };
                category = new Models.Outputs.category
                {
                    categoryid = type.cat.ID.ToString(),
                    categoryname = (type.cat.CategoryName == null) ? "" : type.cat.CategoryName,
                    imageurl = (type.cat.ImageURL == null) ? "" : type.cat.ImageURL,
                    translations = this.getTranslations((type.cat.CategoryName == null) ? "" : type.cat.CategoryName)
                };
                company = new Models.Outputs.company
                {
                    companyid = type.com.ID.ToString(),
                    companyname = (type.com.CompanyName == null) ? "" : type.com.CompanyName,
                    imageurl = (type.com.ImageURL == null) ? "" : type.com.ImageURL,
                    translations = this.getTranslations((type.com.CompanyName == null) ? "" : type.com.CompanyName)
                };
                tone.album = album;
                tone.artist = artist;
                tone.category = category;
                tone.company = company;
                item.tone = tone;
                list.Add(item);
            }
            return list;
        }

        private List<Translations> getTranslations(List<string> texts)
        {
            List<Translations> list = new List<Translations>();
            foreach (string str in texts)
            {
                list.AddRange(this.getTranslations(str));
            }
            return list;
        }

        public List<iBand.Models.Outputs.Subscription.PaymentChannelDTO> getPaymentChannels(int countryid, int serviceid, int operatorid, int servicetype)
        {
            List<iBand.Models.Outputs.Subscription.PaymentChannelDTO> PaymentChannels = new List<iBand.Models.Outputs.Subscription.PaymentChannelDTO>();

            var rows = (from bpc in db.BillingPaymentsConfigurations
                        join bp in db.BillingPayments on bpc.BillingPaymentID equals bp.ID
                        where bpc.CountryID == countryid && (bpc.OperatorID == null || bpc.OperatorID == operatorid) &&
                        (bpc.ServiceType == null || bpc.ServiceType == servicetype) && (bpc.ServiceID == null || bpc.ServiceID == serviceid)
                        select new { bp, bpc }).AsEnumerable().Select(x => new Models.Outputs.Subscription.PaymentChannelDTO { PaymentID = x.bpc.ID.ToString(), PaymentType = x.bp.PaymentType.Trim(), PaymentName = x.bp.PaymentName, Status = x.bp.Status.ToString() }).ToList();

            return rows;
        }

        private List<Translations> getTranslations(string sourcetext)
        {
            List<Translations> list = new List<Translations>();
            return (from t in this.db.Translations
                    where t.SourceText.ToLower().Equals(sourcetext) && (t.Status == true)
                    orderby t.LanguageCode
                    select new Translations { languagecode = t.LanguageCode, sourcetext = t.SourceText, translatedtext = t.TranslatedText }).ToList<Translations>();
        }

        #endregion Private Methods
    }


}
