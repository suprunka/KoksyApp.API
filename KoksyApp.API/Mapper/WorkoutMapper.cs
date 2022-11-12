using KoksyApp.API.Models;
using KoksyApp.API.Models.DTO;
using KoksyApp.Dtos.Models;

namespace KoksyApp.API.Mapper;

public static  class WorkoutMapper
{
   public static WorkoutDayDto Map(WorkoutDay dto)
   {
       return new WorkoutDayDto
       {
           Name = dto.Name,
           LastOpened = dto.LastOpened,
           
       };
   }
}