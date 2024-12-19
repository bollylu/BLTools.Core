namespace BLTools.Core.Text;

public interface ITextBox {

  string Title { get; set; }
  string Content { get; set; }

  TTextBoxOptions Options { get; }

  string Render();

}