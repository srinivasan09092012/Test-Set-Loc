namespace HPP.OneClick.XAML.Migration.Log4Net
{
  public interface IOneClickLog
  {
    void Info(object msg);

    void Warning(object msg);

    void Error(object msg);

    void Debug(object msg);
  }
}
