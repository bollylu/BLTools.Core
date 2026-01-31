namespace BLTools.Test.Diagnostics;

#region --- IInnerData --------------------------------------------
public interface IInnerData {
  string Name { get; init; }
  string Description { get; set; }
  int Age { get; set; }
  char Code { get; set; }
}

public class TDataHolder : ALoggable, IInnerData {
  public string Name { get; init; } = "toto";
  public string Description { get; set; } = "le heros";
  public int Age { get; set; } = 42;
  public char Code { get; set; } = 'X';

  public bool IsDone = true;

  private readonly long ExecutionCount = 38;
}

public class TDataHolder2 : IInnerData {
  public string Name { get; init; } = "Paul";
  public string Description { get; set; } = "et Virginie";
  public int Age { get; set; } = 76;
  public char Code { get; set; } = 'Y';
}
#endregion --- IInnerData -----------------------------------------

#region --- IData --------------------------------------------
public interface IData {
  IInnerData InnerData { get; }
  IInnerData InnerData2 { get; }
  List<IMoreData> MoreData { get; }
}

public class TComplexDataHolder : IData {
  public string Name { get; set; } = "Movie";
  public IInnerData InnerData { get; set; } = new TDataHolder();
  public IInnerData InnerData2 { get; set; } = new TDataHolder2();
  public IInnerData InnerData3 { get; set; } = new TDataHolder2() { Name = "Other", Description = "blabla ...", Age = 33 };
  public List<IMoreData> MoreData { get; } = [];

  public TComplexDataHolder() {
    MoreData.Add(new TMoreData());
  }
}

public class TComplexDataHolder2 : IData {
  public IInnerData InnerData { get; set; } = new TDataHolder();
  public IInnerData InnerData2 { get; set; } = new TDataHolder() { Name = "Other", Description = "blabla ...", Age = 33 };
  public List<IMoreData> MoreData { get; } = [];

  private readonly double PiValue = 3.1416d;

  static public TComplexDataHolder2 Empty => new TComplexDataHolder2();

  public TComplexDataHolder2() {
    MoreData.Add(new TMoreData());
    MoreData.Add(new TMoreData2());
  }
}
#endregion --- IData --------------------------------------------

#region --- IMoreData --------------------------------------------
public interface IMoreData {
  DateTime ExecTime { get; set; }
}

public class TMoreData : IMoreData {
  public DateTime ExecTime { get; set; } = DateTime.Now;
}

public class TMoreData2 : IMoreData {
  public DateTime ExecTime { get; set; } = DateTime.Now;
  public TDataHolder InnerData { get; } = new TDataHolder() {
    Name = "Did it",
    Description = "Again",
    Age = 28,
    IsDone = true
  };
}

#endregion --- IMoreData --------------------------------------------



public struct SPerson {
  public string Name { get; set; }
  public int Id { get; set; }
  public IMoreData MoreData { get; set; }
}

public class TCustomers : List<SPerson> {
}

public class TContactFile {
  public TCustomers Customers { get; } = [
    new SPerson() {Id=1, Name="Luc"},
    new SPerson() {Id=2, Name="Brilly"}
  ];
}