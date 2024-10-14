using Hangfire;
using Hangfire.SqlServer;
using FengShuiKoi_DAO;

var builder = WebApplication.CreateBuilder(args);

// Tạo một instance của DbContext
var dbContext = new SWP391_FengShuiKoiConsulting_DBContext();

// Lấy chuỗi kết nối từ DbContext
var connectionString = dbContext.Database.GetConnectionString();

// Cấu hình Hangfire với chuỗi kết nối từ DbContext
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(connectionString));

// Thêm Hangfire server
builder.Services.AddHangfireServer();

var app = builder.Build();

// Các middleware khác...

// Cấu hình và sử dụng Hangfire Dashboard
app.UseHangfireDashboard();

// Schedule Hangfire jobs
HangfireJobScheduler.ScheduleRecurringJobs();

// Các cấu hình khác...

app.Run();
