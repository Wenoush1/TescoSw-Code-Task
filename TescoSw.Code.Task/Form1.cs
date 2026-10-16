
using TescoSw.Code.Task.Models;
using TescoSw.Code.Task.Services;

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
        using (OpenFileDialog dialog = new OpenFileDialog())
        {
            dialog.Title = "Select an XML file with sold cars";
            dialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                List<Car> cars = CarXmlReader.Load(dialog.FileName);
                List<ModelSummary> summaries = SalesCalculator.GetWeekendTotalsByModel(cars);

                ShowCars(cars);
                ShowResults(summaries);
                lblFilePath.Text = dialog.FileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show("The file could not be loaded:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void ShowCars(List<Car> cars)
    {
        dgvCars.DataSource = cars;

        GetCarsColumn(nameof(Car.ModelName)).HeaderText = "Model name";
        GetCarsColumn(nameof(Car.SaleDate)).HeaderText = "Sale date";
        GetCarsColumn(nameof(Car.SaleDate)).DefaultCellStyle.Format = "d.M.yyyy";
        GetCarsColumn(nameof(Car.Price)).DefaultCellStyle.Format = "N2";
        GetCarsColumn(nameof(Car.VatRate)).HeaderText = "VAT (%)";
    }

    private DataGridViewColumn GetCarsColumn(string propertyName)
    {
        return dgvCars.Columns[propertyName]
            ?? throw new InvalidOperationException(
                $"Column '{propertyName}' was not found in the cars grid.");
    }

    private void ShowResults(List<ModelSummary> summaries)
    {
        dgvResults.Rows.Clear();

        foreach (ModelSummary summary in summaries)
        {
            dgvResults.Rows.Add(
                summary.ModelName + Environment.NewLine + summary.TotalExcludingVat.ToString("N2"),
                Environment.NewLine + summary.TotalIncludingVat.ToString("N2"));
        }
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
