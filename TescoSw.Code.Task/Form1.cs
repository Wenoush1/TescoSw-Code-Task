
namespace TescoSw.Code.Task;
public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        SetUpResultsGridStyle();
    }

    private void btnLoad_Click(object sender, EventArgs e)
    {
    }

    private void SetUpResultsGridStyle()
    {
        dgvResults.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
        dgvResults.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
        dgvResults.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;

        dgvResults.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
        dgvResults.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        dgvResults.DefaultCellStyle.SelectionBackColor = Color.White;
        dgvResults.DefaultCellStyle.SelectionForeColor = Color.Black;
    }
}
