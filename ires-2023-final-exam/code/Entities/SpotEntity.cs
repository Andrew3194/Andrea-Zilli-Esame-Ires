namespace Ires2023Exam.Entities;

public class SpotEntity
{
    public int Id { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    
    public int? ContainerId { get; set; }
    public ContainerEntity Container { get; set; }
}
