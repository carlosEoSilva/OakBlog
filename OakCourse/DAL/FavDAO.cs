﻿using DTO;
using System;
using System.Linq;

namespace DAL
{
    public class FavDAO 
    {
        public FavDTO GetFav()
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                //por algum motivo o campo DeletedDate não ficar nulo no banco.
                FavLogoTitle fav = db.FavLogoTitles.First();

                FavDTO dto = new FavDTO
                {
                    ID = fav.ID,
                    Title = fav.Title,
                    Logo = fav.Logo,
                    Fav = fav.Fav
                };

                return dto;
            }
                
        }

        public FavDTO UpdateFav(FavDTO model)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    FavLogoTitle fav = db.FavLogoTitles.First();

                    FavDTO dto = new FavDTO
                    {
                        ID = fav.ID,
                        Fav = fav.Fav,
                        Logo = fav.Logo,
                        Title = model.Title
                    };

                    //?BUG? gera um erro quando tenta atualizar o title...
                    fav.Title = model.Title;

                    if (model.Logo != null)
                    {
                        //caminho da imagem do logo
                        fav.Logo = model.Logo;
                    }

                    if (model.Fav != null)
                    {
                        //caminho da imagem do fav
                        fav.Fav = model.Fav;
                    }

                    db.SaveChanges();
                    return dto;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
                
        }
    }
}
