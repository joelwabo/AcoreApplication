namespace AcoreApplication.Model
{
    public interface ISegmentService
    {
        bool InsertSegment();
        bool DeleteSegment(Segment segment);
        bool UpdateSegment(Segment segment);
    }
}