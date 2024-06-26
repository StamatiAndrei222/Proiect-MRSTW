using AutoMapper;
using EnglishCourses.Domain.Entities.Course;
using EnglishCourses.Domain.Entities.CourseChapter;
using EnglishCourses.Domain.Entities.Teacher;
using EnglishCourses.Domain.Entities.User;
using EnglishCourses.Web.Models.Course;
using EnglishCourses.Web.Models.CourseChapter;
using EnglishCourses.Web.Models.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace EnglishCourses.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            InitializeAutoMapper();
        }


        protected static void InitializeAutoMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UDbTable, UserMinimal>();

                cfg.CreateMap<TeacherDbTable, TeacherBrief>()
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src => Convert.ToBase64String(src.ProfilePicture)));
                cfg.CreateMap<RegisterTeacher, RegisterTeacherData>();
                cfg.CreateMap<EditTeacher, EditTeacherData>();
                cfg.CreateMap<TeacherDbTable, EditTeacher>()
                .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore());

                cfg.CreateMap<RegisterCourse, RegisterCourseData>();
                cfg.CreateMap<EditCourse, EditCourseData>();
                cfg.CreateMap<CourseDbTable, CourseBrief>()
                .ForMember(dest => dest.DisplayImage, opt => opt.MapFrom(src => Convert.ToBase64String(src.DisplayImage)));
                cfg.CreateMap<CourseDbTable, CourseComplete>()
                .ForMember(dest => dest.DisplayImage, opt => opt.Ignore());
                cfg.CreateMap<CourseDbTable, CourseSuggestionModel>()
                .ForMember(dest => dest.DisplayImage, opt => opt.MapFrom(src => Convert.ToBase64String(src.DisplayImage)));
                cfg.CreateMap<CourseDbTable, CourseDetails>()
                .ForMember(dest => dest.DisplayImage, opt => opt.MapFrom(src => Convert.ToBase64String(src.DisplayImage)));

                cfg.CreateMap<ChapterDbTable, ChapterBrief>();
                cfg.CreateMap<RegisterChapter, RegisterChapterData>();
                cfg.CreateMap<EditChapter, EditChapterData>();
                cfg.CreateMap<EditChapter, ChapterDbTable>();
                cfg.CreateMap<ChapterDbTable, ChapterComplete>();


            });

        }

    }
}