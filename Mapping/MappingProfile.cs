using System;
using AutoMapper;
using SurveySystem.Models;
using SurveySystem.Models.Views;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SurveySystem.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //domin to view
            CreateMap<Group, GroupView>();
            CreateMap<Survey, SurveyView>()
            .ForMember(sv => sv.Type, opt => opt.MapFrom(s => new TypeView { Id = s.TypeId.ToString(), Name = s.Type == null ? null : s.Type.Name, Description = s.Type == null ? null : s.Type.Description }));

            CreateMap<SurveyGroup, GroupView>()
            .ForMember(gv => gv.Id, opt => opt.MapFrom(sg => sg.GroupId))
            .ForMember(gv => gv.Name, opt => opt.MapFrom(sg => sg.Group.Name))
            .ForMember(gv => gv.Description, opt => opt.MapFrom(sg => sg.Group.Description));

            CreateMap<Question, QuestionView>()
            .ForMember(qv => qv.Answers, opt => opt.MapFrom(q => q.Answers.Select(a => a.Value)));

            CreateMap<SurveyAnswerTemplate, TemplateView>();


            CreateMap<IEnumerable<SurveyReport>, SurveyReportView>()
            .ForMember(srv => srv.Question, opt => opt.MapFrom(l => l.FirstOrDefault().Question))
            .ForMember(srv => srv.Participants, opt => opt.MapFrom(l => l.Select(i => new ParticipantView
            {
                Answers = i.Answers.Select(a => new AnswerView
                {
                    Name = a.Value
                }).ToList(),
                Voter = new VoterView
                {
                    Name = i.Voter.Name,
                    Phone = i.Voter.Phone
                }
            })))
            .ForMember(srv => srv.Templates, opt => opt.MapFrom(l => l.FirstOrDefault().Templates.Select(t => new TemplateView
            {
                Name = t.Name,
                Id = t.Id.ToString()
            })))
            .AfterMap((sr, srv) =>
            {
                foreach (var template in srv.Templates)
                {
                    template.Percent = srv.Participants.Count(p => p.Answers.Select(a => a.Name).Contains(template.Id));
                }
                var sum = srv.Templates.Sum(t => t.Percent);

                foreach (var template in srv.Templates)
                {
                    template.Percent = 100 * template.Percent / sum;
                }
        
                foreach (var participant in srv.Participants)
                {
                    foreach (var answer in participant.Answers)
                    {
                        var result = srv.Templates.FirstOrDefault(t => t.Id == answer.Name)?.Name;
                        if (!string.IsNullOrEmpty(result))
                            answer.Name = result;
                    }
                }
            });


            //view to domain
            CreateMap<VoterView, Voter>();
            CreateMap<GroupView, Group>();
            CreateMap<VoteView, Vote>();

            CreateMap<QuestionView, Question>()
            .ForMember(q => q.Answers, opt => opt.MapFrom(qv => qv.Answers.Select(a => new Answer { Value = a })));

            CreateMap<SurveyView, Survey>()
            .ForMember(s => s.TypeId, opt => opt.MapFrom(sv => sv.Type.Id))
            .ForMember(s => s.Type, opt => opt.Ignore())
            .ForMember(s => s.Templates, opt => opt.Ignore())
            .ForMember(s => s.Groups, opt => opt.Ignore())
            .AfterMap((sv, s) =>
            {
                // Remove unselected groups
                var removedGroups = s.Groups.Where(gc => !sv.Groups.Select(g => g.Id).Contains(gc.GroupId.ToString())).ToList();
                foreach (var g in removedGroups)
                    s.Groups.Remove(g);
                // Add new groups
                var addedGroups = sv.Groups.Where(gv => !s.Groups.Any(g => g.GroupId.ToString() == gv.Id)).Select(gv => new SurveyGroup { GroupId = new Guid(gv.Id) });
                foreach (var g in addedGroups)
                    s.Groups.Add(g);

                // Remove unselected templates
                var removedTemplates = s.Templates.Where(tc => !sv.Templates.Select(t => t.Id).Contains(tc.Id.ToString())).ToList();
                foreach (var t in removedTemplates)
                    s.Templates.Remove(t);
                // Add new templates
                var addedTemplates = sv.Templates.Where(tv => !s.Templates.Any(t => t.Id.ToString() == tv.Id)).Select(tv => new SurveyAnswerTemplate { Id = Guid.NewGuid(), Name = tv.Name });
                foreach (var t in addedTemplates)
                    s.Templates.Add(t);
            });

        }
    }
}