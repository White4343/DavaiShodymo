using DavaiShodymo.Categories;
using DavaiShodymo.EventImagePlacements;
using DavaiShodymo.Tags;

namespace DavaiShodymo.Events.GetEventById;

public record GetEventByIdResponse(int Id, DateTime DateStart, DateTime DateEnd, DateTime DateStamp, 
    string Name, string? Description, string? Location, List<EventImageCommand>? EventImages, 
    float TotalRating, int? UserId, string UserFullName, List<Tag>? Tags, List<Category>? Categories);