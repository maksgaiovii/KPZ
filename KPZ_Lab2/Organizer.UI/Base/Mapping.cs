using AutoMapper;
using KPZ_Lab2.Models;
using Organizer.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizer.UI.Base
{
    public class Mapping
    {
        private readonly IMapper _mapper;

        public Mapping()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DataModel, DataViewModel>();
                cfg.CreateMap<DataViewModel, DataModel>();

                cfg.CreateMap<Book, BookViewModel>();
                cfg.CreateMap<BookViewModel, Book>();

                cfg.CreateMap<PrintingHouse, PrintingHouseViewModel>();
                cfg.CreateMap<PrintingHouseViewModel, PrintingHouse>();

                cfg.CreateMap<TeamMember, TeamMemberViewModel>();
                cfg.CreateMap<TeamMemberViewModel, TeamMember>();

                cfg.CreateMap<Text, TextViewModel>();
                cfg.CreateMap<TextViewModel, Text>();
            });

            _mapper = config.CreateMapper();
        }

        public IMapper GetMapper()
        {
            return _mapper;
        }
    }

}
