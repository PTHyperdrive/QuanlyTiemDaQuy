using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyTiemDaQuy.BLL.Services;
using QuanLyTiemDaQuy.Models;

namespace QuanLyTiemDaQuy.Forms
{
    /// <summary>
    /// Form quản lý và hiển thị giá thị trường đá quý
    /// Hỗ trợ lấy giá từ API và cập nhật vào database
    /// </summary>
    public partial class MarketPriceForm : Form
    {
        private readonly MarketPriceApiService _apiService;
        private readonly PricingService _pricingService;

        public MarketPriceForm()
        {
            InitializeComponent();
            _apiService = new MarketPriceApiService();
            _pricingService = new PricingService();
            SetupDataGridView();
        }

        #region Form Events

        private void MarketPriceForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private async void btnSyncApi_Click(object sender, EventArgs e)
        {
            await SyncFromApiAsync();
        }

        #endregion

        #region UI Setup

        private void SetupDataGridView()
        {
            dgvPrices.EnableHeadersVisualStyles = false;
            dgvPrices.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 70);
            dgvPrices.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvPrices.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvPrices.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPrices.ColumnHeadersHeight = 40;

            dgvPrices.DefaultCellStyle.BackColor = Color.FromArgb(40, 40, 60);
            dgvPrices.DefaultCellStyle.ForeColor = Color.White;
            dgvPrices.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 150, 136);
            dgvPrices.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvPrices.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvPrices.RowTemplate.Height = 35;

            dgvPrices.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 75);
        }

        private void SetupColumns()
        {
            dgvPrices.Columns.Clear();
            
            dgvPrices.Columns.Add("StoneTypeName", "Loại đá");
            dgvPrices.Columns.Add("PriceUsd", "Giá (USD/ct)");
            dgvPrices.Columns.Add("PriceVnd", "Giá (VND/ct)");
            dgvPrices.Columns.Add("Source", "Nguồn");
            dgvPrices.Columns.Add("Notes", "Ghi chú");
            dgvPrices.Columns.Add("LastUpdated", "Cập nhật");

            dgvPrices.Columns["StoneTypeName"].Width = 130;
            dgvPrices.Columns["PriceUsd"].Width = 100;
            dgvPrices.Columns["PriceVnd"].Width = 140;
            dgvPrices.Columns["Source"].Width = 150;
            dgvPrices.Columns["Notes"].Width = 200;
            dgvPrices.Columns["LastUpdated"].Width = 120;

            dgvPrices.Columns["PriceUsd"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPrices.Columns["PriceVnd"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPrices.Columns["PriceVnd"].DefaultCellStyle.ForeColor = Color.FromArgb(0, 200, 83);
            dgvPrices.Columns["PriceVnd"].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        }

        #endregion

        #region Data Loading

        private void LoadData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                SetupColumns();

                // Lấy giá từ database hiện tại
                var prices = _pricingService.GetAllMarketPrices();
                var exchangeRate = _apiService.GetCurrentExchangeRate();

                lblExchangeRate.Text = $"💱 Tỷ giá: 1 USD = {exchangeRate:N0} VND";

                dgvPrices.Rows.Clear();
                foreach (var price in prices)
                {
                    // Tính giá USD từ giá VND
                    decimal priceUsd = exchangeRate > 0 ? price.BasePricePerCarat / exchangeRate : 0;
                    
                    dgvPrices.Rows.Add(
                        price.StoneTypeName,
                        $"${priceUsd:N2}",
                        $"{price.BasePricePerCarat:N0} ₫",
                        "Database",
                        GemstoneReferencePrices.GetReferencePrice(price.StoneTypeName).Notes,
                        price.LastUpdated.ToString("dd/MM/yyyy HH:mm")
                    );
                }

                lblLastUpdate.Text = $"Cập nhật: {DateTime.Now:HH:mm dd/MM/yyyy}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        #endregion

        #region API Sync

        private async Task SyncFromApiAsync()
        {
            try
            {
                btnSyncApi.Enabled = false;
                btnSyncApi.Text = "⏳ Đang tải...";
                Cursor = Cursors.WaitCursor;

                // Fetch from API
                var result = await _apiService.FetchAllPricesAsync();
                
                if (!result.Success)
                {
                    MessageBox.Show($"Lỗi: {result.Message}", "Lỗi API", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Update exchange rate display
                lblExchangeRate.Text = $"💱 Tỷ giá: 1 USD = {result.ExchangeRateUsdVnd:N0} VND";

                // Show fetched data in grid
                SetupColumns();
                dgvPrices.Rows.Clear();
                
                foreach (var price in result.Prices)
                {
                    dgvPrices.Rows.Add(
                        price.StoneTypeName,
                        $"${price.PricePerCaratUsd:N2}",
                        $"{price.PricePerCaratVnd:N0} ₫",
                        price.Source,
                        price.Notes,
                        price.LastUpdated.ToString("dd/MM/yyyy HH:mm")
                    );
                }

                lblLastUpdate.Text = $"Cập nhật: {result.FetchedAt:HH:mm dd/MM/yyyy} ({result.Source})";

                // Ask user to sync to database
                var confirmResult = MessageBox.Show(
                    $"Đã lấy giá cho {result.Prices.Count} loại đá từ {result.Source}.\n\n" +
                    $"Tỷ giá USD/VND: {result.ExchangeRateUsdVnd:N0}\n\n" +
                    "Bạn có muốn cập nhật giá vào database không?",
                    "Xác nhận cập nhật",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmResult == DialogResult.Yes)
                {
                    var syncResult = _apiService.SyncToDatabase(result);
                    
                    if (syncResult.Success)
                    {
                        MessageBox.Show(syncResult.Message, "Thành công", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblInfo.Text = $"✅ {syncResult.Message}";
                    }
                    else
                    {
                        MessageBox.Show(syncResult.Message, "Lỗi", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy dữ liệu từ API: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSyncApi.Text = "🌐 Cập nhật từ API";
                btnSyncApi.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        #endregion
    }
}
