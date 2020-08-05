using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace ChoiceA.Services
{
    public interface IGroupsService
    {
        string[] Groups { get; }
    }

    public class GroupsService : IGroupsService
    {
        public string[] Groups { private set; get; }


        public GroupsService()
        {
            Groups = File.ReadAllLines(@"C:\Users\TrLuxsus\source\repos\Choise\ChoiceA\Static\Groups.txt");
        }
    }

    public static class GroupsServiceExtention
    {
        public static IServiceCollection AddGroups(this IServiceCollection services)
            => services.AddTransient<IGroupsService, GroupsService>();
    }
}
