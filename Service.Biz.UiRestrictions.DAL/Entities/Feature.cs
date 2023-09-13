using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Service.Biz.UiRestrictions.DAL.Entities;

public class Feature
{
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;
}
