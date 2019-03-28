namespace AcoreApplication.Model
{
    public interface ISegmentService
    {
        bool Insert(Segment segment);
        bool Delete(Segment segment);
        bool Update(Segment segment);
    }
}