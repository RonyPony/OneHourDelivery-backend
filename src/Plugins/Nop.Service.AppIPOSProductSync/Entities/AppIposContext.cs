using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nop.Service.AppIPOSSync.Helpers;

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class AppIposContext : DbContext
    {
        public AppIposContext()
        {
        }

        public AppIposContext(DbContextOptions<AppIposContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfiguration configuration = ConfigurationHelper.GetConfiguration();

                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
        }

        public virtual DbSet<AccountsPayable> AccountsPayables { get; set; }
        public virtual DbSet<Agent> Agents { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<AppointmentDetail> AppointmentDetails { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<BillDetail> BillDetails { get; set; }
        public virtual DbSet<BillDetailProductBatch> BillDetailProductBatches { get; set; }
        public virtual DbSet<BillEscrow> BillEscrows { get; set; }
        public virtual DbSet<BillProductDetail> BillProductDetails { get; set; }
        public virtual DbSet<BillRefund> BillRefunds { get; set; }
        public virtual DbSet<BillRefundDetail> BillRefundDetails { get; set; }
        public virtual DbSet<CashRegister> CashRegisters { get; set; }
        public virtual DbSet<CashRegisterType> CashRegisterTypes { get; set; }
        public virtual DbSet<ChangeRoom> ChangeRooms { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<ColorationBox> ColorationBoxes { get; set; }
        public virtual DbSet<ColorationBoxDetail> ColorationBoxDetails { get; set; }
        public virtual DbSet<Command> Commands { get; set; }
        public virtual DbSet<CommandComment> CommandComments { get; set; }
        public virtual DbSet<CommandComplement> CommandComplements { get; set; }
        public virtual DbSet<CommandDetailCopy> CommandDetailCopies { get; set; }
        public virtual DbSet<CommandSended> CommandSendeds { get; set; }
        public virtual DbSet<ComplementMenu> ComplementMenus { get; set; }
        public virtual DbSet<CorrectionReason> CorrectionReasons { get; set; }
        public virtual DbSet<CorrectionWarehouse> CorrectionWarehouses { get; set; }
        public virtual DbSet<CorrectionWarehouseDetail> CorrectionWarehouseDetails { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<CreditCardType> CreditCardTypes { get; set; }
        public virtual DbSet<CreditNote> CreditNotes { get; set; }
        public virtual DbSet<CreditPayment> CreditPayments { get; set; }
        public virtual DbSet<CurrencyType> CurrencyTypes { get; set; }
        public virtual DbSet<Customize> Customizes { get; set; }
        public virtual DbSet<DiscountCard> DiscountCards { get; set; }
        public virtual DbSet<DiscountType> DiscountTypes { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<ErrorLog> ErrorLogs { get; set; }
        public virtual DbSet<Escrow> Escrows { get; set; }
        public virtual DbSet<ExchangeRate> ExchangeRates { get; set; }
        public virtual DbSet<Floor> Floors { get; set; }
        public virtual DbSet<FloorPlanObject> FloorPlanObjects { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<GamePrice> GamePrices { get; set; }
        public virtual DbSet<GetExistenceAverageInKardex> GetExistenceAverageInKardices { get; set; }
        public virtual DbSet<GetExistenceMenuItem> GetExistenceMenuItems { get; set; }
        public virtual DbSet<GetToReportBill> GetToReportBills { get; set; }
        public virtual DbSet<GiftTable> GiftTables { get; set; }
        public virtual DbSet<GiftTableDetail> GiftTableDetails { get; set; }
        public virtual DbSet<Goal> Goals { get; set; }
        public virtual DbSet<GoalRegion> GoalRegions { get; set; }
        public virtual DbSet<GuestBook> GuestBooks { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual DbSet<InvoiceDetailProductBatch> InvoiceDetailProductBatches { get; set; }
        public virtual DbSet<Kardex> Kardices { get; set; }
        public virtual DbSet<KardexOperation> KardexOperations { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<MenuItem> MenuItems { get; set; }
        public virtual DbSet<MenuItemCategory> MenuItemCategories { get; set; }
        public virtual DbSet<MenuItemDetail> MenuItemDetails { get; set; }
        public virtual DbSet<MenuItemSubcategory> MenuItemSubcategories { get; set; }
        public virtual DbSet<MyCompany> MyCompanies { get; set; }
        public virtual DbSet<Object> Objects { get; set; }
        public virtual DbSet<OpeningCash> OpeningCashes { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderActivesList> OrderActivesLists { get; set; }
        public virtual DbSet<OrderAnnulled> OrderAnnulleds { get; set; }
        public virtual DbSet<OrderAnnulledDetail> OrderAnnulledDetails { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<OrderDetailActivesList> OrderDetailActivesLists { get; set; }
        public virtual DbSet<OrderDetailComplement> OrderDetailComplements { get; set; }
        public virtual DbSet<OrderDetailProductBatch> OrderDetailProductBatches { get; set; }
        public virtual DbSet<OrderProductDeleted> OrderProductDeleteds { get; set; }
        public virtual DbSet<OrderProductDetail> OrderProductDetails { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<PackageDetail> PackageDetails { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<PaymentConcept> PaymentConcepts { get; set; }
        public virtual DbSet<PaymentDetail> PaymentDetails { get; set; }
        public virtual DbSet<PaymentType> PaymentTypes { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductBatch> ProductBatches { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductChange> ProductChanges { get; set; }
        public virtual DbSet<ProductChangeDetail> ProductChangeDetails { get; set; }
        public virtual DbSet<ProductCombo> ProductCombos { get; set; }
        public virtual DbSet<ProductComboDetail> ProductComboDetails { get; set; }
        public virtual DbSet<ProductInOut> ProductInOuts { get; set; }
        public virtual DbSet<ProductInOutDetail> ProductInOutDetails { get; set; }
        public virtual DbSet<ProductReturn> ProductReturns { get; set; }
        public virtual DbSet<ProductReturnDetail> ProductReturnDetails { get; set; }
        public virtual DbSet<ProductSupplier> ProductSuppliers { get; set; }
        public virtual DbSet<Proform> Proforms { get; set; }
        public virtual DbSet<ProformDetail> ProformDetails { get; set; }
        public virtual DbSet<ProformProductDetail> ProformProductDetails { get; set; }
        public virtual DbSet<Promotion> Promotions { get; set; }
        public virtual DbSet<PromotionDetail> PromotionDetails { get; set; }
        public virtual DbSet<Rate> Rates { get; set; }
        public virtual DbSet<Receipt> Receipts { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Remission> Remissions { get; set; }
        public virtual DbSet<RemissionDetail> RemissionDetails { get; set; }
        public virtual DbSet<RepaymentDetail> RepaymentDetails { get; set; }
        public virtual DbSet<ReportReservation> ReportReservations { get; set; }
        public virtual DbSet<Requisition> Requisitions { get; set; }
        public virtual DbSet<RequisitionDetail> RequisitionDetails { get; set; }
        public virtual DbSet<ReservationBook> ReservationBooks { get; set; }
        public virtual DbSet<ReservationBookDetail> ReservationBookDetails { get; set; }
        public virtual DbSet<ReservationBookDetailList> ReservationBookDetailLists { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomList> RoomLists { get; set; }
        public virtual DbSet<RoomState> RoomStates { get; set; }
        public virtual DbSet<RoomStatusRegister> RoomStatusRegisters { get; set; }
        public virtual DbSet<RoomType> RoomTypes { get; set; }
        public virtual DbSet<Season> Seasons { get; set; }
        public virtual DbSet<Series> Series { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<ServiceCategory> ServiceCategories { get; set; }
        public virtual DbSet<ServiceItemDetail> ServiceItemDetails { get; set; }
        public virtual DbSet<ServiceSubCategory> ServiceSubCategories { get; set; }
        public virtual DbSet<Serviciotemp> Serviciotemps { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<SessionLog> SessionLogs { get; set; }
        public virtual DbSet<Station> Stations { get; set; }
        public virtual DbSet<Supplyer> Supplyers { get; set; }
        public virtual DbSet<SystemAccess> SystemAccesses { get; set; }
        public virtual DbSet<SystemAccessRole> SystemAccessRoles { get; set; }
        public virtual DbSet<SystemModule> SystemModules { get; set; }
        public virtual DbSet<SystemOption> SystemOptions { get; set; }
        public virtual DbSet<SystemOptionRole> SystemOptionRoles { get; set; }
        public virtual DbSet<SystemRole> SystemRoles { get; set; }
        public virtual DbSet<Table> Tables { get; set; }
        public virtual DbSet<TableCashRegister> TableCashRegisters { get; set; }
        public virtual DbSet<TableReservation> TableReservations { get; set; }
        public virtual DbSet<TableSection> TableSections { get; set; }
        public virtual DbSet<TableSectionDetail> TableSectionDetails { get; set; }
        public virtual DbSet<TableType> TableTypes { get; set; }
        public virtual DbSet<Tax> Taxes { get; set; }
        public virtual DbSet<TourOperator> TourOperators { get; set; }
        public virtual DbSet<Trademark> Trademarks { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }
        public virtual DbSet<VendorWorkArea> VendorWorkAreas { get; set; }
        public virtual DbSet<View2> View2s { get; set; }
        public virtual DbSet<ViewAmountCreditByBill> ViewAmountCreditByBills { get; set; }
        public virtual DbSet<ViewAmountDevByBill> ViewAmountDevByBills { get; set; }
        public virtual DbSet<ViewAmountRemissionByClient> ViewAmountRemissionByClients { get; set; }
        public virtual DbSet<ViewBill> ViewBills { get; set; }
        public virtual DbSet<ViewBill1> ViewBills1 { get; set; }
        public virtual DbSet<ViewBillDetail> ViewBillDetails { get; set; }
        public virtual DbSet<ViewBillRefund> ViewBillRefunds { get; set; }
        public virtual DbSet<ViewBillRefundDetail> ViewBillRefundDetails { get; set; }
        public virtual DbSet<ViewBillingAccountStatus> ViewBillingAccountStatuses { get; set; }
        public virtual DbSet<ViewBreakdownCloseCash> ViewBreakdownCloseCashes { get; set; }
        public virtual DbSet<ViewCashRegister> ViewCashRegisters { get; set; }
        public virtual DbSet<ViewCashRegisterRole> ViewCashRegisterRoles { get; set; }
        public virtual DbSet<ViewClient> ViewClients { get; set; }
        public virtual DbSet<ViewClientsStruct> ViewClientsStructs { get; set; }
        public virtual DbSet<ViewCloseCash> ViewCloseCashes { get; set; }
        public virtual DbSet<ViewCorrectionWarehouse> ViewCorrectionWarehouses { get; set; }
        public virtual DbSet<ViewCorrectionWarehouseDetail> ViewCorrectionWarehouseDetails { get; set; }
        public virtual DbSet<ViewCreditBill> ViewCreditBills { get; set; }
        public virtual DbSet<ViewCreditBillBalance> ViewCreditBillBalances { get; set; }
        public virtual DbSet<ViewCreditNote> ViewCreditNotes { get; set; }
        public virtual DbSet<ViewCreditNoteInfo> ViewCreditNoteInfos { get; set; }
        public virtual DbSet<ViewCreditNoteInfoDetail> ViewCreditNoteInfoDetails { get; set; }
        public virtual DbSet<ViewCreditsUnpaidDate> ViewCreditsUnpaidDates { get; set; }
        public virtual DbSet<ViewDebitInvoice> ViewDebitInvoices { get; set; }
        public virtual DbSet<ViewGeneralTable> ViewGeneralTables { get; set; }
        public virtual DbSet<ViewGoal> ViewGoals { get; set; }
        public virtual DbSet<ViewGoalRegion> ViewGoalRegions { get; set; }
        public virtual DbSet<ViewInformePosInventario> ViewInformePosInventarios { get; set; }
        public virtual DbSet<ViewInformePosVenta> ViewInformePosVentas { get; set; }
        public virtual DbSet<ViewInvoice> ViewInvoices { get; set; }
        public virtual DbSet<ViewInvoiceDetail> ViewInvoiceDetails { get; set; }
        public virtual DbSet<ViewKadex> ViewKadices { get; set; }
        public virtual DbSet<ViewListProduct> ViewListProducts { get; set; }
        public virtual DbSet<ViewListProduct1> ViewListProducts1 { get; set; }
        public virtual DbSet<ViewListProductByWarehouseFastBilling> ViewListProductByWarehouseFastBillings { get; set; }
        public virtual DbSet<ViewListRemision> ViewListRemisions { get; set; }
        public virtual DbSet<ViewListWithholding> ViewListWithholdings { get; set; }
        public virtual DbSet<ViewMarginsMenuItem> ViewMarginsMenuItems { get; set; }
        public virtual DbSet<ViewMarginsProduct> ViewMarginsProducts { get; set; }
        public virtual DbSet<ViewMarginsRoom> ViewMarginsRooms { get; set; }
        public virtual DbSet<ViewMarginsService> ViewMarginsServices { get; set; }
        public virtual DbSet<ViewMenu> ViewMenus { get; set; }
        public virtual DbSet<ViewMenuAtribb> ViewMenuAtribbs { get; set; }
        public virtual DbSet<ViewOrder> ViewOrders { get; set; }
        public virtual DbSet<ViewOrderDetail> ViewOrderDetails { get; set; }
        public virtual DbSet<ViewPayment> ViewPayments { get; set; }
        public virtual DbSet<ViewPaymentDetail> ViewPaymentDetails { get; set; }
        public virtual DbSet<ViewProductInOut> ViewProductInOuts { get; set; }
        public virtual DbSet<ViewProductInOutDetail> ViewProductInOutDetails { get; set; }
        public virtual DbSet<ViewProductSupplier> ViewProductSuppliers { get; set; }
        public virtual DbSet<ViewProform> ViewProforms { get; set; }
        public virtual DbSet<ViewProformDetail> ViewProformDetails { get; set; }
        public virtual DbSet<ViewReceipt> ViewReceipts { get; set; }
        public virtual DbSet<ViewRecoveryBilling> ViewRecoveryBillings { get; set; }
        public virtual DbSet<ViewRemission> ViewRemissions { get; set; }
        public virtual DbSet<ViewRemissionDetail> ViewRemissionDetails { get; set; }
        public virtual DbSet<ViewRepInvoicedetail> ViewRepInvoicedetails { get; set; }
        public virtual DbSet<ViewRepaymentDetail> ViewRepaymentDetails { get; set; }
        public virtual DbSet<ViewSupplier> ViewSuppliers { get; set; }
        public virtual DbSet<ViewTableReservation> ViewTableReservations { get; set; }
        public virtual DbSet<ViewTotalDetailByBill> ViewTotalDetailByBills { get; set; }
        public virtual DbSet<ViewTotalPaymentReceiptByBill> ViewTotalPaymentReceiptByBills { get; set; }
        public virtual DbSet<ViewTotalRefoundByBill> ViewTotalRefoundByBills { get; set; }
        public virtual DbSet<ViewUser> ViewUsers { get; set; }
        public virtual DbSet<ViewWaiterPoint> ViewWaiterPoints { get; set; }
        public virtual DbSet<ViewWarehouseProduct> ViewWarehouseProducts { get; set; }
        public virtual DbSet<ViewWarehouseTranfer> ViewWarehouseTranfers { get; set; }
        public virtual DbSet<ViewWarehouseTranferDetail> ViewWarehouseTranferDetails { get; set; }
        public virtual DbSet<ViewWorkOrder> ViewWorkOrders { get; set; }
        public virtual DbSet<ViewWorkOrderDetail> ViewWorkOrderDetails { get; set; }
        public virtual DbSet<ViewWorkOrderProductDetail> ViewWorkOrderProductDetails { get; set; }
        public virtual DbSet<Warehouse> Warehouses { get; set; }
        public virtual DbSet<WarehouseProduct> WarehouseProducts { get; set; }
        public virtual DbSet<WarehouseTranfer> WarehouseTranfers { get; set; }
        public virtual DbSet<WarehouseTranferDetail> WarehouseTranferDetails { get; set; }
        public virtual DbSet<Withholding> Withholdings { get; set; }
        public virtual DbSet<WorkArea> WorkAreas { get; set; }
        public virtual DbSet<WorkOrder> WorkOrders { get; set; }
        public virtual DbSet<WorkOrderDetail> WorkOrderDetails { get; set; }
        public virtual DbSet<WorkOrderProductDetail> WorkOrderProductDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<AccountsPayable>(entity =>
            {
                entity.ToTable("AccountsPayable");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AfterBalancePayment).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Idinvoice).HasColumnName("IDInvoice");

                entity.Property(e => e.PayableDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentAmount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PreviousBalancePayment).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<Agent>(entity =>
            {
                entity.ToTable("Agent");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.CompanyId)
                    .HasColumnName("CompanyID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.DocumentId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DocumentID");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("EMail");

                entity.Property(e => e.Fax)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("Appointment");

                entity.HasIndex(e => e.ClientId, "IX_AppointmentClientID");

                entity.HasIndex(e => e.IsFree, "IX_AppointmentIsOtherState");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.Comment)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<AppointmentDetail>(entity =>
            {
                entity.ToTable("AppointmentDetail");

                entity.HasIndex(e => e.ServiceId, "IX_AppointmentDetailServiceId");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.ToTable("Bill");

                entity.HasIndex(e => e.CashRegisterId, "IX_BillCashRegisterID");

                entity.HasIndex(e => e.Client, "IX_BillClient");

                entity.HasIndex(e => e.BillDate, "IX_BillDate");

                entity.HasIndex(e => e.OpeningCashId, "IX_BillOpeningCashID");

                entity.HasIndex(e => e.Order, "IX_BillOrder");

                entity.HasIndex(e => e.BillPaid, "IX_BillPaid");

                entity.HasIndex(e => e.RegisteredUser, "IX_BillRegisteredUser");

                entity.HasIndex(e => e.SessionId, "IX_BillSessionID");

                entity.HasIndex(e => e.TableId, "IX_BillTableID");

                entity.HasIndex(e => e.ToCredit, "IX_BillToCredit");

                entity.HasIndex(e => e.TourOperator, "IX_BillTourOperator");

                entity.HasIndex(e => new { e.Id, e.ToCredit, e.CreditPaymentDate, e.RegisteredUser, e.BillPaid }, "IX_Bill_StatusRC");

                entity.HasIndex(e => e.IsDetail, "IX_IsDetail");

                entity.HasIndex(e => new { e.ToCredit, e.Annulled }, "IX_NC_Bill_ToCredit_Annulled_ID_Client");

                entity.HasIndex(e => e.Number, "IX_number_Bill")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AnnulledDate).HasColumnType("datetime");

                entity.Property(e => e.AnnulledOpeningCashId).HasColumnName("AnnulledOpeningCashID");

                entity.Property(e => e.BillDate).HasColumnType("datetime");

                entity.Property(e => e.BillDiscount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.BillSubTotal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.BillTax).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.BillTax2).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.BillTips).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.BillTotal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CashRegisterId).HasColumnName("CashRegisterID");

                entity.Property(e => e.Comment)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreditPaymentDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerDiscount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.DiscountCardId).HasColumnName("DiscountCardID");

                entity.Property(e => e.ExchangeMoney).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ExchangeMoneySec).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ExchangeRate).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OpeningCashId).HasColumnName("OpeningCashID");

                entity.Property(e => e.Order).HasDefaultValueSql("((1))");

                entity.Property(e => e.RegionId).HasColumnName("RegionID");

                entity.Property(e => e.SendCost).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.SessionId).HasColumnName("SessionID");

                entity.Property(e => e.StationId).HasColumnName("StationID");

                entity.Property(e => e.TableId).HasColumnName("TableID");

                entity.Property(e => e.TotalExtra).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<BillDetail>(entity =>
            {
                entity.ToTable("BillDetail");

                entity.HasIndex(e => e.RemissionId, "<Name of Missing Index, sysname,>");

                entity.HasIndex(e => e.CertificateId, "IX_BillDetailCertificateID");

                entity.HasIndex(e => e.IdreservationDetail, "IX_BillDetailIDReservationDetail");

                entity.HasIndex(e => e.IsExtra, "IX_BillDetailIsExtra");

                entity.HasIndex(e => e.IsOpenBar, "IX_BillDetailIsOpenBar");

                entity.HasIndex(e => e.IsSpecialDrink, "IX_BillDetailIsSpecialDrink");

                entity.HasIndex(e => e.MenuItem, "IX_BillDetailMenuItem");

                entity.HasIndex(e => e.PackageId, "IX_BillDetailPackageID");

                entity.HasIndex(e => e.PlayTimeId, "IX_BillDetailPlayTimeID");

                entity.HasIndex(e => e.ProductId, "IX_BillDetailProductID");

                entity.HasIndex(e => e.RateId, "IX_BillDetailRateID");

                entity.HasIndex(e => e.Room, "IX_BillDetailRoom");

                entity.HasIndex(e => e.RoomIdExtra, "IX_BillDetailRoomIdExtra");

                entity.HasIndex(e => e.Service, "IX_BillDetailService");

                entity.HasIndex(e => e.Unit, "IX_BillDetailUnit");

                entity.HasIndex(e => e.VendorId, "IX_BillDetailVendorID");

                entity.HasIndex(e => e.Bill, "IX_BillDetail_Bill");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AdultQuantity)
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.AmountByService).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AmountByServicePercent).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.BillDiscount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.BillDiscountPercent).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.CertificateId).HasColumnName("CertificateID");

                entity.Property(e => e.ChildQuantity)
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Comment)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CommissionVendorPercent).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CustomerDiscount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CustomerDiscountPercent).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.DiscountCardAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DiscountCardPercent).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.GiftTableId).HasColumnName("GiftTableID");

                entity.Property(e => e.IdreservationDetail).HasColumnName("IDReservationDetail");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PackageId).HasColumnName("PackageID");

                entity.Property(e => e.PlayTimeId).HasColumnName("PlayTimeID");

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PriceAdultExtra)
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PriceChildrenExtra)
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ProductDiscount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ProductDiscountPercent).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.QuantityAdultExtra)
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.QuantityChildrenExtra)
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.QuantityHours).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.RateId).HasColumnName("RateID");

                entity.Property(e => e.RatePrice)
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RemissionId).HasColumnName("RemissionID");

                entity.Property(e => e.StarDate).HasColumnType("datetime");

                entity.Property(e => e.SubTotal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tax1).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tax1Percent).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.Tax2).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tax2Percent).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.Tax3).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tax3Percent).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.VendorId).HasColumnName("VendorID");

                entity.Property(e => e.WarehouseId).HasColumnName("WarehouseID");

                entity.Property(e => e.WorkAreaId).HasColumnName("WorkAreaID");
            });

            modelBuilder.Entity<BillDetailProductBatch>(entity =>
            {
                entity.ToTable("BillDetailProductBatch");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Batch)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BillDetailId).HasColumnName("BillDetailID");

                entity.Property(e => e.ExpirationDate).HasColumnType("date");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<BillEscrow>(entity =>
            {
                entity.ToTable("BillEscrow");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<BillProductDetail>(entity =>
            {
                entity.ToTable("BillProductDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BillDetailId).HasColumnName("BillDetailID");

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.MenuItemId).HasColumnName("MenuItemID");

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<BillRefund>(entity =>
            {
                entity.ToTable("BillRefund");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AfterBalance)
                    .HasColumnType("numeric(18, 2)")
                    .HasComputedColumnSql("([dbo].[GetAfterBalancePaymentDev]([ID],[BillID]))", false);

                entity.Property(e => e.BillId).HasColumnName("BillID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.OpeningCashId).HasColumnName("OpeningCashID");

                entity.Property(e => e.PreviousBalance)
                    .HasColumnType("numeric(18, 2)")
                    .HasComputedColumnSql("([dbo].[GetPreviousBalancePaymentDev]([ID],[BillID]))", false);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<BillRefundDetail>(entity =>
            {
                entity.ToTable("BillRefundDetail");

                entity.HasComment("");

                entity.HasIndex(e => e.BillRefundId, "IXNC_BillRefundDetailBillRefundID_QuantityRefund_Price");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AmountByService).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.BillDetailId).HasColumnName("BillDetailID");

                entity.Property(e => e.BillRefundId).HasColumnName("BillRefundID");

                entity.Property(e => e.CommissionVendor).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Discount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.QuantityRefund)
                    .HasColumnType("numeric(18, 2)")
                    .HasComment("cantidad devuelta");

                entity.Property(e => e.QuantityTmp)
                    .HasColumnType("numeric(18, 2)")
                    .HasComment("cantidad del detalle");

                entity.Property(e => e.SubTotal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tax1).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tax2).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<CashRegister>(entity =>
            {
                entity.ToTable("CashRegister");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccessCode)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Comment)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Company).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ParentCashRegister).HasDefaultValueSql("((0))");

                entity.Property(e => e.SeriesBillingId)
                    .HasColumnName("SeriesBillingID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SeriesReceiptId).HasColumnName("SeriesReceiptID");

                entity.Property(e => e.SeriesRemisionId).HasColumnName("SeriesRemisionID");
            });

            modelBuilder.Entity<CashRegisterType>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CashRegisterType");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ChangeRoom>(entity =>
            {
                entity.ToTable("ChangeRoom");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Remark).IsUnicode(false);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Client");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Balance)
                    .HasColumnType("numeric(18, 2)")
                    .HasComputedColumnSql("([dbo].[GetCreditBalanceClient]([ID],[Credit]))", false);

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyId)
                    .HasColumnName("CompanyID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Credit).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.DiscountValue).HasColumnType("numeric(9, 2)");

                entity.Property(e => e.DocumentId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DocumentID");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("EMail");

                entity.Property(e => e.Fax)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RecordDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Specialty)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.VendorId).HasColumnName("VendorID");
            });

            modelBuilder.Entity<ColorationBox>(entity =>
            {
                entity.ToTable("ColorationBox");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Oxidant).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<ColorationBoxDetail>(entity =>
            {
                entity.ToTable("ColorationBoxDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ColorationBoxId).HasColumnName("ColorationBoxID");

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<Command>(entity =>
            {
                entity.ToTable("Command");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Comment)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CommandComment>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CommandComment");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CommandId).HasColumnName("CommandID");

                entity.Property(e => e.Comment)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");
            });

            modelBuilder.Entity<CommandComplement>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CommandComplement");

                entity.Property(e => e.ComplmentMenuId)
                    .HasColumnName("ComplmentMenuID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.MenuItemId).HasColumnName("MenuItemID");

                entity.Property(e => e.Ref).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<CommandDetailCopy>(entity =>
            {
                entity.ToTable("CommandDetailCopy");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CopyCommandId).HasColumnName("CopyCommandID");

                entity.Property(e => e.OriginalCommandId).HasColumnName("OriginalCommandID");
            });

            modelBuilder.Entity<CommandSended>(entity =>
            {
                entity.ToTable("CommandSended");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CommandId).HasColumnName("CommandID");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.End).HasColumnType("smalldatetime");

                entity.Property(e => e.MenuItemId).HasColumnName("MenuItemID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.Product)
                    .IsRequired()
                    .HasMaxLength(125)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PromotionId).HasColumnName("PromotionID");

                entity.Property(e => e.PromotionName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Start)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TableId)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("TableID");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("UserID");
            });

            modelBuilder.Entity<ComplementMenu>(entity =>
            {
                entity.ToTable("ComplementMenu");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MenuItemId).HasColumnName("MenuItemID");
            });

            modelBuilder.Entity<CorrectionReason>(entity =>
            {
                entity.ToTable("CorrectionReason");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(300);
            });

            modelBuilder.Entity<CorrectionWarehouse>(entity =>
            {
                entity.ToTable("CorrectionWarehouse");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Comment).IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Reference).HasMaxLength(50);
            });

            modelBuilder.Entity<CorrectionWarehouseDetail>(entity =>
            {
                entity.ToTable("CorrectionWarehouseDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Existence).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Input).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Output).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.QuantityReal).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CreditCardType>(entity =>
            {
                entity.ToTable("CreditCardType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CreditNote>(entity =>
            {
                entity.ToTable("CreditNote");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.AnnulledDate).HasColumnType("datetime");

                entity.Property(e => e.Balance)
                    .HasColumnType("numeric(18, 2)")
                    .HasComputedColumnSql("([dbo].[GetBalanceCerticate]([ID]))", false);

                entity.Property(e => e.Comment)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.IsRefound).HasColumnName("isRefound");

                entity.Property(e => e.RealCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RegisteredUser).HasDefaultValueSql("((1))");

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CreditPayment>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AnnulledDate).HasColumnType("datetime");

                entity.Property(e => e.OpeningCashId).HasColumnName("OpeningCashID");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CurrencyType>(entity =>
            {
                entity.ToTable("CurrencyType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Simbol)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Customize>(entity =>
            {
                entity.HasKey(e => e.IdCustomize)
                    .HasName("PK_tab_customize");

                entity.ToTable("Customize");

                entity.HasIndex(e => e.Field, "IX_tab_customize")
                    .IsUnique();

                entity.Property(e => e.Field)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<DiscountCard>(entity =>
            {
                entity.ToTable("DiscountCard");

                entity.HasIndex(e => e.Number, "IX_Number_DiscountCard")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.DateBill).HasColumnType("datetime");

                entity.Property(e => e.DateRegistered).HasColumnType("datetime");

                entity.Property(e => e.Discount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DiscountType>(entity =>
            {
                entity.ToTable("DiscountType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(225)
                    .IsUnicode(false);

                entity.Property(e => e.Value).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<DocumentType>(entity =>
            {
                entity.ToTable("DocumentType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ErrorLog>(entity =>
            {
                entity.ToTable("ErrorLog");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.ErrorSource)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ErrorType)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Station)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Escrow>(entity =>
            {
                entity.ToTable("Escrow");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Amount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Annulled).HasDefaultValueSql("((0))");

                entity.Property(e => e.AnnulledDate).HasColumnType("datetime");

                entity.Property(e => e.EscrowDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ExchangeRate>(entity =>
            {
                entity.ToTable("ExchangeRate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ExchangeRateDate).HasColumnType("datetime");

                entity.Property(e => e.ExchangeRateValue).HasColumnType("numeric(18, 4)");
            });

            modelBuilder.Entity<Floor>(entity =>
            {
                entity.ToTable("Floor");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FloorPlanObject>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("FloorPlanObject");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.Xpos).HasColumnName("XPos");

                entity.Property(e => e.Ypos).HasColumnName("YPos");
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.ToTable("Game");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<GamePrice>(entity =>
            {
                entity.ToTable("GamePrice");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.GameId).HasColumnName("GameID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PriceHour).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PriceMinute).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<GetExistenceAverageInKardex>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("GetExistenceAverageInKardex");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.MenuItemId).HasColumnName("MenuItemID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Quantity).HasColumnType("numeric(38, 6)");
            });

            modelBuilder.Entity<GetExistenceMenuItem>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("GetExistenceMenuItems");

                entity.Property(e => e.MenuItemId).HasColumnName("MenuItemID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Quantity).HasColumnType("numeric(38, 6)");
            });

            modelBuilder.Entity<GetToReportBill>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("GetToReportBill");

                entity.Property(e => e.AmountByService).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnnulledDate).HasColumnType("datetime");

                entity.Property(e => e.BillDate).HasColumnType("datetime");

                entity.Property(e => e.Comment)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreditPaymentDate).HasColumnType("datetime");

                entity.Property(e => e.DescriptionDetails)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Discount).HasColumnType("numeric(21, 2)");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.NameClient)
                    .HasMaxLength(511)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.SubTotal).HasColumnType("numeric(37, 4)");

                entity.Property(e => e.Tax1).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tax2).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tax3).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Total).HasColumnType("numeric(38, 4)");
            });

            modelBuilder.Entity<GiftTable>(entity =>
            {
                entity.HasKey(e => e.IdgiftTable);

                entity.ToTable("GiftTable");

                entity.Property(e => e.IdgiftTable).HasColumnName("IDGiftTable");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.NameGiftTable)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<GiftTableDetail>(entity =>
            {
                entity.HasKey(e => e.IdgiftTableDetail);

                entity.ToTable("GiftTableDetail");

                entity.Property(e => e.IdgiftTableDetail).HasColumnName("IDGiftTableDetail");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.IdgiftTable).HasColumnName("IDGiftTable");

                entity.Property(e => e.Idproduct).HasColumnName("IDProduct");

                entity.Property(e => e.Idunit).HasColumnName("IDUnit");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<Goal>(entity =>
            {
                entity.ToTable("Goal");

                entity.HasComment("Metas por area de trabajo");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MenuGoal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.MonthGoal).HasComment("mes de la meta");

                entity.Property(e => e.ProductGoal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.RoomGoal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ServiceGoal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.VendorId).HasColumnName("VendorID");

                entity.Property(e => e.WorkAreaId).HasColumnName("WorkAreaID");

                entity.Property(e => e.YearGoal).HasComment("año de la meta");
            });

            modelBuilder.Entity<GoalRegion>(entity =>
            {
                entity.ToTable("GoalRegion");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MenuGoal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ProductGoal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.RegionId).HasColumnName("RegionID");

                entity.Property(e => e.RoomGoal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ServiceGoal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.VendorId).HasColumnName("VendorID");
            });

            modelBuilder.Entity<GuestBook>(entity =>
            {
                entity.ToTable("GuestBook");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CheckIn).HasColumnType("datetime");

                entity.Property(e => e.CheckOut).HasColumnType("datetime");

                entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("Invoice");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreditAmount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DateEndCredit).HasColumnType("datetime");

                entity.Property(e => e.ExchangeRate)
                    .HasColumnType("numeric(18, 4)")
                    .HasDefaultValueSql("((0.0000))");

                entity.Property(e => e.IsPaid)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Reference)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UnpaidAmount)
                    .HasColumnType("numeric(18, 2)")
                    .HasComputedColumnSql("([dbo].[GetCreditBalanceSupplyer]([ID],[CreditAmount]))", false);
            });

            modelBuilder.Entity<InvoiceDetail>(entity =>
            {
                entity.ToTable("InvoiceDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CostCp).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Quantity)
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SubTotal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.SubTotalCp).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tax).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.TaxValue).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.TaxValueCp).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<InvoiceDetailProductBatch>(entity =>
            {
                entity.ToTable("InvoiceDetailProductBatch");

                entity.HasIndex(e => e.InvoiceDetailId, "IX_InvoiceDetailProductBatch");

                entity.HasIndex(e => e.ProductId, "IX_InvoiceDetailProductBatch_1");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Batch)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ExpirationDate).HasColumnType("date");

                entity.Property(e => e.InvoiceDetailId).HasColumnName("InvoiceDetailID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<Kardex>(entity =>
            {
                entity.ToTable("Kardex");

                entity.HasIndex(e => new { e.Operation, e.Date }, "IX_NC_Kardex_Operation_Date_Product_Imput_Output");

                entity.HasIndex(e => new { e.Product, e.Date }, "IX_NC_Kardex_Product_Date_CostUnitOperation");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CostFinal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CostInitial).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CostUnitOperation).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Input)
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.Output)
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.QuantityFinal)
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.QuantityInitial).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<KardexOperation>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.ToTable("Language");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MenuItem>(entity =>
            {
                entity.ToTable("MenuItem");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Code)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CommandId).HasColumnName("CommandID");

                entity.Property(e => e.Discount)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ElaborationCost).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Food).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsOpenBar).HasColumnName("isOpenBar");

                entity.Property(e => e.MenuItemCategoryId).HasColumnName("MenuItemCategoryID");

                entity.Property(e => e.MenuItemSubCategoryId)
                    .HasColumnName("MenuItemSubCategoryID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PriceHappy).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PromotionCommission).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.QtyMin).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Quantity)
                    .HasColumnType("numeric(18, 2)")
                    .HasComputedColumnSql("([dbo].[GetExistenceByMenuItems]([ID]))", false);

                entity.Property(e => e.SpecialDrinkPrices).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TotalCost)
                    .HasColumnType("numeric(18, 2)")
                    .HasComputedColumnSql("([dbo].[GetTotalCost]([ID]))", false);
            });

            modelBuilder.Entity<MenuItemCategory>(entity =>
            {
                entity.ToTable("MenuItemCategory");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MenuItemDetail>(entity =>
            {
                entity.ToTable("MenuItemDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cost)
                    .HasColumnType("numeric(18, 2)")
                    .HasComputedColumnSql("([dbo].[GetIngredientCost]([ProductID],[UnitID],[Quantity]))", false);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UnitId).HasColumnName("UnitID");
            });

            modelBuilder.Entity<MenuItemSubcategory>(entity =>
            {
                entity.ToTable("MenuItemSubcategory");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Color).HasDefaultValueSql("('-255')");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.MenuItemCategoryId).HasColumnName("MenuItemCategoryID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MyCompany>(entity =>
            {
                entity.ToTable("MyCompany");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.AutorizacionCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.GobId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("GobID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Object>(entity =>
            {
                entity.HasKey(e => e.Idobject);

                entity.ToTable("Object");

                entity.Property(e => e.Idobject).HasColumnName("IDObject");

                entity.Property(e => e.ImageObject).IsRequired();

                entity.Property(e => e.NameObject)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OpeningCash>(entity =>
            {
                entity.ToTable("OpeningCash");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cash).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Check).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Credit).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CreditCard).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CreditNote).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.EndDate).HasColumnType("smalldatetime");

                entity.Property(e => e.ExchangeMoney).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ExchangeMoneySec).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.GiftCertificate).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.MainCurrencyValue).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Payments).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Rate).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.Recipts).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.SecondaryCurrencyValue).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.StartDate).HasColumnType("smalldatetime");

                entity.Property(e => e.TotalBilling).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.TotalCash).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.TotalDiscount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.TotalTax1).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.TotalTax2).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.TotalTax3).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AnnulledDate).HasColumnType("datetime");

                entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");

                entity.Property(e => e.ClientBalance).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ClientName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ClosingDate).HasColumnType("datetime");

                entity.Property(e => e.CreditPaymentDate).HasColumnType("datetime");

                entity.Property(e => e.DateSendToBill).HasColumnType("datetime");

                entity.Property(e => e.DiscountPercent).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.IdorderRoom).HasColumnName("IDOrderRoom");

                entity.Property(e => e.InProcess)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.OpeningCashId).HasColumnName("OpeningCashID");

                entity.Property(e => e.OpeningDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RegionId).HasColumnName("RegionID");

                entity.Property(e => e.ReservationId).HasColumnName("ReservationID");

                entity.Property(e => e.SessionId).HasColumnName("SessionID");

                entity.Property(e => e.SpecialDiscount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.StationId).HasColumnName("StationID");

                entity.Property(e => e.TableId).HasColumnName("TableID");
            });

            modelBuilder.Entity<OrderActivesList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OrderActivesList");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.Id)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.NameClient)
                    .HasMaxLength(511)
                    .IsUnicode(false);

                entity.Property(e => e.NumberRoom)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OpeningDate).HasColumnType("datetime");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");
            });

            modelBuilder.Entity<OrderAnnulled>(entity =>
            {
                entity.ToTable("OrderAnnulled");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Date)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<OrderAnnulledDetail>(entity =>
            {
                entity.ToTable("OrderAnnulledDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MenuItemId).HasColumnName("MenuItemID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("OrderDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AdultQuantity)
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.AmountByService).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AmountByServicePercent).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.ChildQuantity)
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Code)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Comment)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CommissionVendor).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CustomerDiscount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CustomerDiscountPercent).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.GiftTableId).HasColumnName("GiftTableID");

                entity.Property(e => e.IdreservationDetail).HasColumnName("IDReservationDetail");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.PackageId).HasColumnName("PackageID");

                entity.Property(e => e.PlayTimeId).HasColumnName("PlayTimeID");

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PriceAdultExtra)
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PriceChildrenExtra)
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ProductDiscount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ProductDiscountPercent).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.QuantityAdultExtra)
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.QuantityChildrenExtra)
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.QuantityHours).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.RateId).HasColumnName("RateID");

                entity.Property(e => e.RatePrice)
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RemissionId).HasColumnName("RemissionID");

                entity.Property(e => e.StarDate).HasColumnType("datetime");

                entity.Property(e => e.Tax1).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tax1Percent).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.Tax2).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tax2Percent).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.Tax3).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tax3Percent).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.UnCommandedQty).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.VaucherService)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.VendorId).HasColumnName("VendorID");

                entity.Property(e => e.WarehouseId).HasColumnName("WarehouseID");

                entity.Property(e => e.WorkAreaId).HasColumnName("WorkAreaID");
            });

            modelBuilder.Entity<OrderDetailActivesList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OrderDetailActivesList");

                entity.Property(e => e.AmountByService).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CustomerDiscount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DiscountTotal).HasColumnType("numeric(19, 2)");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Id)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.MontoFact).HasColumnType("numeric(37, 4)");

                entity.Property(e => e.Number)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.PackageId).HasColumnName("PackageID");

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ProductDiscount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.QuantityHours).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.StarDate).HasColumnType("datetime");

                entity.Property(e => e.SubTotalConDescuento).HasColumnType("numeric(38, 4)");

                entity.Property(e => e.SubTotalGeneral).HasColumnType("numeric(38, 4)");

                entity.Property(e => e.Tax1).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tax2).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tax3).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OrderDetailComplement>(entity =>
            {
                entity.ToTable("OrderDetailComplement");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MenuItemId).HasColumnName("MenuItemID");

                entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");

                entity.Property(e => e.Quantity).HasColumnType("numeric(9, 2)");
            });

            modelBuilder.Entity<OrderDetailProductBatch>(entity =>
            {
                entity.ToTable("OrderDetailProductBatch");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Batch)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ExpirationDate).HasColumnType("date");

                entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<OrderProductDeleted>(entity =>
            {
                entity.ToTable("OrderProductDeleted");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CashRegisterId).HasColumnName("CashRegisterID");

                entity.Property(e => e.Date)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MenuItemId).HasColumnName("MenuItemID");

                entity.Property(e => e.Motive)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.Quantity).HasColumnType("numeric(9, 2)");

                entity.Property(e => e.SessionId).HasColumnName("SessionID");

                entity.Property(e => e.TableId).HasColumnName("TableID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<OrderProductDetail>(entity =>
            {
                entity.ToTable("OrderProductDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.MenuItemId).HasColumnName("MenuItemID");

                entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            });

            modelBuilder.Entity<Package>(entity =>
            {
                entity.ToTable("Package");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<PackageDetail>(entity =>
            {
                entity.ToTable("PackageDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.HasIndex(e => new { e.Annulled, e.Receipt }, "IXNC_PaymentAnnulledReceipt");

                entity.HasIndex(e => e.Bill, "IX_NC_Payment_Bill_ID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AnnulledDate).HasColumnType("datetime");

                entity.Property(e => e.InitialPayment)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.OpeningCashId).HasColumnName("OpeningCashID");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.SessionId)
                    .HasColumnName("SessionID")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<PaymentConcept>(entity =>
            {
                entity.ToTable("PaymentConcept");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Amount).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<PaymentDetail>(entity =>
            {
                entity.ToTable("PaymentDetail");

                entity.HasIndex(e => e.PaymentType, "IX_NC_PaymentDetail_PaymentType");

                entity.HasIndex(e => new { e.Payment, e.PrimaryAmount }, "IX_PaymentDetail_Payment");

                entity.HasIndex(e => e.PaymentType, "IX_PaymentDetail_PaymentType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Amount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AuthorizationNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Bank)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BankAccount)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BillIdwithholding)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("BillIDWithholding");

                entity.Property(e => e.BillNumberWithholding)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CheckNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreditCardNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ExchangeRate).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.ExpirationDate)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.NumberTransfer)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NumberWithholding)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PorcWithholding).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.PrimaryAmount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.TransferDate).HasColumnType("datetime");

                entity.Property(e => e.WithholdingId).HasColumnName("WithholdingID");
            });

            modelBuilder.Entity<PaymentType>(entity =>
            {
                entity.ToTable("PaymentType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.HasIndex(e => new { e.Locked, e.Active, e.TrademarkId }, "IX_NC_Product_Locked_Active_TrademarkID");

                entity.HasIndex(e => e.Active, "IX_P_Active");

                entity.HasIndex(e => e.Name, "IX_P_Name");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.AlertQuantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.BarCode)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Comment)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Commission).HasColumnType("numeric(9, 4)");

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Discount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.FlagMarginPrice).HasDefaultValueSql("((1))");

                entity.Property(e => e.Location)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.MarginPrice).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Portion)
                    .HasColumnType("numeric(18, 4)")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Presentation)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PricePortion)
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.QuantityBill).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.QuantitySubUnit)
                    .HasColumnType("numeric(18, 2)")
                    .HasComputedColumnSql("([dbo].[QuantitySubUnit]([ID]))", false);

                entity.Property(e => e.QuantityUnit)
                    .HasColumnType("numeric(18, 2)")
                    .HasComputedColumnSql("([dbo].[QuantityUnit]([ID]))", false);

                entity.Property(e => e.TrademarkId).HasColumnName("TrademarkID");

                entity.Property(e => e.Treatment)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProductBatch>(entity =>
            {
                entity.ToTable("ProductBatch");

                entity.HasIndex(e => e.ProductId, "IX_ProductBatch");

                entity.HasIndex(e => e.WarehouseId, "IX_ProductBatch_1");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Batch)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ExpirationDate).HasColumnType("date");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.WarehouseId).HasColumnName("WarehouseID");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable("ProductCategory");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Comment)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProductChange>(entity =>
            {
                entity.ToTable("ProductChange");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AmountToAdd).HasColumnType("money");

                entity.Property(e => e.AmountToTakeOff).HasColumnType("money");

                entity.Property(e => e.ChangeDate).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Difference).HasColumnType("money");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.QuantityToAdd).HasColumnType("money");

                entity.Property(e => e.QuantityToTakeOff).HasColumnType("money");
            });

            modelBuilder.Entity<ProductChangeDetail>(entity =>
            {
                entity.ToTable("ProductChangeDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cost).HasColumnType("money");

                entity.Property(e => e.IsAdd).HasColumnName("isAdd");

                entity.Property(e => e.IsTakeOff).HasColumnName("isTakeOff");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProductCombo>(entity =>
            {
                entity.ToTable("ProductCombo");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Comment)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Discount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Quantity)
                    .HasColumnType("numeric(18, 2)")
                    .HasComputedColumnSql("([dbo].[GetExistenceByProductCombo]([ID]))", false);

                entity.Property(e => e.TotalCost)
                    .HasColumnType("numeric(18, 2)")
                    .HasComputedColumnSql("([dbo].[GetTotalCostCombo]([ID]))", false);
            });

            modelBuilder.Entity<ProductComboDetail>(entity =>
            {
                entity.ToTable("ProductComboDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cost)
                    .HasColumnType("numeric(18, 2)")
                    .HasComputedColumnSql("([dbo].[GetIngredientCost]([ProductID],[UnitID],[Quantity]))", false);

                entity.Property(e => e.ProductComboId).HasColumnName("ProductComboID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UnitId).HasColumnName("UnitID");
            });

            modelBuilder.Entity<ProductInOut>(entity =>
            {
                entity.ToTable("ProductInOut");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AnnulledDate).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Observation)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ReferenceName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ReferenceNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProductInOutDetail>(entity =>
            {
                entity.ToTable("ProductInOutDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.SubTotal).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<ProductReturn>(entity =>
            {
                entity.ToTable("ProductReturn");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Date).HasColumnType("datetime");
            });

            modelBuilder.Entity<ProductReturnDetail>(entity =>
            {
                entity.ToTable("ProductReturnDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProductSupplier>(entity =>
            {
                entity.ToTable("ProductSupplier");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.SupplyerId).HasColumnName("SupplyerID");
            });

            modelBuilder.Entity<Proform>(entity =>
            {
                entity.ToTable("Proform");

                entity.HasComment("Proforma de ventas");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AnnulledDate).HasColumnType("datetime");

                entity.Property(e => e.ClientBalance).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ClientName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ClosingDate).HasColumnType("datetime");

                entity.Property(e => e.Comment)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.DiscountPercent).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.InProcess)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.OpeningCashId).HasColumnName("OpeningCashID");

                entity.Property(e => e.OpeningDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.SessionId).HasColumnName("SessionID");

                entity.Property(e => e.SpecialDiscount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.TableId).HasColumnName("TableID");
            });

            modelBuilder.Entity<ProformDetail>(entity =>
            {
                entity.ToTable("ProformDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AdultQuantity)
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.AmountByService).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AmountByServicePercent).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.ChildQuantity)
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Code)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Comment)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CommissionVendor).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CustomerDiscount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CustomerDiscountPercent).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.IdreservationDetail).HasColumnName("IDReservationDetail");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PackageId).HasColumnName("PackageID");

                entity.Property(e => e.PlayTimeId).HasColumnName("PlayTimeID");

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PriceAdultExtra)
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PriceChildrenExtra)
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ProductDiscount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ProductDiscountPercent).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ProformId).HasColumnName("ProformID");

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.QuantityAdultExtra)
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.QuantityChildrenExtra)
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.QuantityHours).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.RateId).HasColumnName("RateID");

                entity.Property(e => e.RatePrice)
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.StarDate).HasColumnType("datetime");

                entity.Property(e => e.StationId).HasColumnName("StationID");

                entity.Property(e => e.Tax1).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tax1Percent).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.Tax2).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tax2Percent).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.Tax3).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tax3Percent).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.UnCommandedQty).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.VendorId).HasColumnName("VendorID");

                entity.Property(e => e.WorkAreaId).HasColumnName("WorkAreaID");
            });

            modelBuilder.Entity<ProformProductDetail>(entity =>
            {
                entity.ToTable("ProformProductDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.MenuItemId).HasColumnName("MenuItemID");

                entity.Property(e => e.ProformDetailId).HasColumnName("ProformDetailID");

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            });

            modelBuilder.Entity<Promotion>(entity =>
            {
                entity.ToTable("Promotion");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.FromDate).HasColumnType("smalldatetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ToDate).HasColumnType("smalldatetime");
            });

            modelBuilder.Entity<PromotionDetail>(entity =>
            {
                entity.ToTable("PromotionDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PromotionPrice).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<Rate>(entity =>
            {
                entity.ToTable("Rate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ExtraAdult).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ExtraChild).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.QuantityHour)
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Receipt>(entity =>
            {
                entity.ToTable("Receipt");

                entity.HasIndex(e => e.Annulled, "IX_NC_Receipt_Annulled_ID");

                entity.HasIndex(e => e.Number, "IX_number_Receipt")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AfterBalance).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AnnulledDate).HasColumnType("datetime");

                entity.Property(e => e.CashRegisterId).HasColumnName("CashRegisterID");

                entity.Property(e => e.Concept)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ExchangeRate)
                    .HasColumnType("decimal(18, 4)")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Idcliente).HasColumnName("IDCliente");

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OpeningCashId).HasColumnName("OpeningCashID");

                entity.Property(e => e.PreviousBalance).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Reference)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RegistrationDate).HasColumnType("datetime");

                entity.Property(e => e.SessionId).HasColumnName("SessionID");

                entity.Property(e => e.TotalWithholding).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable("Region");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Comment)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Cost).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Remission>(entity =>
            {
                entity.ToTable("Remission");

                entity.HasIndex(e => new { e.Anulled, e.IsPaid }, "IX_NC_Remission_Anulled_IsPaid_ID_ClientID");

                entity.HasIndex(e => new { e.ClientId, e.Anulled, e.IsPaid }, "IX_NC_Remission_ClientID_Anulled_IsPaid");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AnnulledDate).HasColumnType("datetime");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Observation)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.VendorId).HasColumnName("VendorID");

                entity.Property(e => e.WarehouseId).HasColumnName("WarehouseID");
            });

            modelBuilder.Entity<RemissionDetail>(entity =>
            {
                entity.ToTable("RemissionDetail");

                entity.HasIndex(e => e.RemissionId, "IX_NC_RemissionDetail_RemissionID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.RemissionId).HasColumnName("RemissionID");

                entity.Property(e => e.SubTotal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tax).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.TaxPorc).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.Total).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<RepaymentDetail>(entity =>
            {
                entity.ToTable("RepaymentDetail");

                entity.HasIndex(e => e.BillId, "IXNC_RepaymentDetailBillID");

                entity.HasIndex(e => new { e.ReceiptId, e.BillId }, "IX_RepaymentDetail_rc");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AfterBalance)
                    .HasColumnType("numeric(18, 2)")
                    .HasComputedColumnSql("([dbo].[GetAfterBalancePayment]([ReceiptID],[BillID]))", false);

                entity.Property(e => e.AfterBalancePayment).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.BillId).HasColumnName("BillID");

                entity.Property(e => e.Ncprorated)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("NCProrated")
                    .HasComputedColumnSql("([dbo].[GetMontoProrrateadoNCbyIdBillReceipt]([BillID],[ReceiptID]))", false);

                entity.Property(e => e.PaymentAmount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PreviousBalance)
                    .HasColumnType("numeric(18, 2)")
                    .HasComputedColumnSql("([dbo].[GetPreviousBalancePayment]([ReceiptID],[BillID]))", false);

                entity.Property(e => e.PreviousBalancePayment).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ReceiptId).HasColumnName("ReceiptID");
            });

            modelBuilder.Entity<ReportReservation>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ReportReservations");

                entity.Property(e => e.AdultQuantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ChildQuantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.DateReservation).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Idreservation).HasColumnName("IDReservation");

                entity.Property(e => e.IdreservationDetail).HasColumnName("IDReservationDetail");

                entity.Property(e => e.NameClient)
                    .HasMaxLength(511)
                    .IsUnicode(false);

                entity.Property(e => e.NameRoom)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.PackageId).HasColumnName("PackageID");

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PriceAdultExtra).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PriceChildrenExtra).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.QuantityAdultExtra).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.QuantityChildrenExtra).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.RateId).HasColumnName("RateID");

                entity.Property(e => e.RatePrice).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.StarDate).HasColumnType("datetime");

                entity.Property(e => e.StateReservation)
                    .HasMaxLength(2)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Requisition>(entity =>
            {
                entity.ToTable("Requisition");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AgentId).HasColumnName("AgentID");

                entity.Property(e => e.AnnulledDate).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Observation)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RequisitionDetail>(entity =>
            {
                entity.ToTable("RequisitionDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.RequisitionId).HasColumnName("RequisitionID");

                entity.Property(e => e.SubTotal).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<ReservationBook>(entity =>
            {
                entity.ToTable("ReservationBook");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateCanceled).HasColumnType("datetime");

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.SessionId).HasColumnName("SessionID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<ReservationBookDetail>(entity =>
            {
                entity.ToTable("ReservationBookDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AdultQuantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ChildQuantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.DiscountValue).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.PackageId).HasColumnName("PackageID");

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PriceAdultExtra).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PriceChildrenExtra).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.QuantityAdultExtra).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.QuantityChildrenExtra).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.QuantityRate)
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.RateId).HasColumnName("RateID");

                entity.Property(e => e.RatePrice).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.StarDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ReservationBookDetailList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ReservationBookDetailList");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.NameFloor)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NameRoom)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NumberFloor)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NumberRoom)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PriceRoom).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ReservationBookDetailId).HasColumnName("ReservationBookDetailID");

                entity.Property(e => e.ReservationBookId).HasColumnName("ReservationBookID");

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.Property(e => e.StarDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Room");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneExtension)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<RoomList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("RoomList");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneExtension)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.RoomStateName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RoomTypeName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RoomState>(entity =>
            {
                entity.ToTable("RoomState");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Reserved).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<RoomStatusRegister>(entity =>
            {
                entity.ToTable("RoomStatusRegister");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.StarDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<RoomType>(entity =>
            {
                entity.ToTable("RoomType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Comment)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<Season>(entity =>
            {
                entity.ToTable("Season");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.BeginDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Series>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Separator)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.SeriesType).HasComment("1 = bill, 2 = receipt, 3 = remision");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Service");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Code)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Cost)
                    .HasColumnType("numeric(18, 2)")
                    .HasComputedColumnSql("([dbo].[GetTotalCostService]([ID]))", false);

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Discount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ElaborationCost).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PromotionPrice).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.RegisteredUser).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<ServiceCategory>(entity =>
            {
                entity.ToTable("ServiceCategory");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ServiceItemDetail>(entity =>
            {
                entity.ToTable("ServiceItemDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cost)
                    .HasColumnType("numeric(18, 2)")
                    .HasComputedColumnSql("([dbo].[GetIngredientCost]([ProductID],[UnitID],[Quantity]))", false);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.UnitId).HasColumnName("UnitID");
            });

            modelBuilder.Entity<ServiceSubCategory>(entity =>
            {
                entity.ToTable("ServiceSubCategory");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Descrption)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Serviciotemp>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("serviciotemp");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Precio)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("precio");
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.ToTable("Session");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EndDate).HasColumnType("smalldatetime");

                entity.Property(e => e.StartDate).HasColumnType("smalldatetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<SessionLog>(entity =>
            {
                entity.ToTable("SessionLog");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Activity)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Entry)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.EntryDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Host)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Manager)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SessionId).HasColumnName("SessionID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Station>(entity =>
            {
                entity.ToTable("Station");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Supplyer>(entity =>
            {
                entity.ToTable("Supplyer");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Balance)
                    .HasColumnType("numeric(18, 2)")
                    .HasComment("0");

                entity.Property(e => e.Comment)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Contact)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Credit).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SystemAccess>(entity =>
            {
                entity.ToTable("SystemAccess");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Interfaz)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Screen)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SystemAccessRole>(entity =>
            {
                entity.ToTable("SystemAccessRole");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<SystemModule>(entity =>
            {
                entity.ToTable("SystemModule");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SystemOption>(entity =>
            {
                entity.ToTable("SystemOption");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Interfaz)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SystemOptionRole>(entity =>
            {
                entity.ToTable("SystemOptionRole");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<SystemRole>(entity =>
            {
                entity.ToTable("SystemRole");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Table>(entity =>
            {
                entity.ToTable("Table");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Comment)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.GamePrice)
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.Lenght).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RentPrice).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Width).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Xpos).HasColumnName("XPos");

                entity.Property(e => e.Ypos).HasColumnName("YPos");
            });

            modelBuilder.Entity<TableCashRegister>(entity =>
            {
                entity.ToTable("TableCashRegister");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<TableReservation>(entity =>
            {
                entity.ToTable("TableReservation");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.Comment)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.TableId).HasColumnName("TableID");
            });

            modelBuilder.Entity<TableSection>(entity =>
            {
                entity.ToTable("TableSection");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TableSectionDetail>(entity =>
            {
                entity.ToTable("TableSectionDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Idobject).HasColumnName("IDObject");

                entity.Property(e => e.IdtableSection).HasColumnName("IDTableSection");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.Xpos).HasColumnName("XPos");

                entity.Property(e => e.Ypos).HasColumnName("YPos");
            });

            modelBuilder.Entity<TableType>(entity =>
            {
                entity.ToTable("TableType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tax>(entity =>
            {
                entity.ToTable("Tax");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Value).HasColumnType("numeric(9, 4)");
            });

            modelBuilder.Entity<TourOperator>(entity =>
            {
                entity.ToTable("TourOperator");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Balance)
                    .HasColumnType("numeric(18, 2)")
                    .HasComputedColumnSql("([dbo].[GetCreditBalanceTourOperator]([ID],[Credit]))", false);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Commission).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Credit).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.DiscountValue).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Email)
                    .HasMaxLength(225)
                    .IsUnicode(false)
                    .HasColumnName("EMail");

                entity.Property(e => e.Fax)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Login)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(225)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.WebSite)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Trademark>(entity =>
            {
                entity.ToTable("Trademark");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.ToTable("Unit");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Login, "LoginIX")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Company).HasDefaultValueSql("((1))");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.VendorId).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.ToTable("Vendor");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.Commission).HasColumnType("numeric(9, 4)");

                entity.Property(e => e.CompanyId)
                    .HasColumnName("CompanyID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.DocumentId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DocumentID");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("EMail");

                entity.Property(e => e.Fax)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductCommission).HasColumnType("numeric(9, 4)");

                entity.Property(e => e.ServiceCategoryId)
                    .HasColumnName("ServiceCategoryID")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<VendorWorkArea>(entity =>
            {
                entity.ToTable("VendorWorkArea");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.VendorId).HasColumnName("VendorID");

                entity.Property(e => e.WorkAreaId).HasColumnName("WorkAreaID");
            });

            modelBuilder.Entity<View2>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_2");

                entity.Property(e => e.CashRegisterName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewAmountCreditByBill>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_AmountCredit_By_Bill");

                entity.Property(e => e.AmountCredit).HasColumnType("numeric(38, 2)");
            });

            modelBuilder.Entity<ViewAmountDevByBill>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_AmountDev_By_Bill");

                entity.Property(e => e.AmountDev).HasColumnType("numeric(38, 4)");

                entity.Property(e => e.BillId).HasColumnName("BillID");
            });

            modelBuilder.Entity<ViewAmountRemissionByClient>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_AmountRemission_By_Client");

                entity.Property(e => e.AmountRemission).HasColumnType("numeric(38, 4)");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");
            });

            modelBuilder.Entity<ViewBill>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewBill");

                entity.Property(e => e.AddressClient)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.AmountByCredit).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AmountByService).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.AnnulledDate).HasColumnType("datetime");

                entity.Property(e => e.AnnulledUserName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.BillDate).HasColumnType("datetime");

                entity.Property(e => e.ClientPostalCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CodeClient)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Comment)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreditPaymentDate).HasColumnType("datetime");

                entity.Property(e => e.DatePaid).HasColumnType("datetime");

                entity.Property(e => e.Discount).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.DocumentIdclient)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DocumentIDClient");

                entity.Property(e => e.DocumentTypeClient)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.EmailClient)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("EMailClient");

                entity.Property(e => e.ExchangeMoney).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ExchangeMoneySec).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ExchangeRate).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.NameClient)
                    .HasMaxLength(511)
                    .IsUnicode(false);

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OpeningCashId).HasColumnName("OpeningCashID");

                entity.Property(e => e.PhoneClient)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrimaryName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PrimarySimbol)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.RegionId).HasColumnName("RegionID");

                entity.Property(e => e.RegionName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.SecondaryName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SecondarySimbol)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.StationId).HasColumnName("StationID");

                entity.Property(e => e.StationName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SubTotal).HasColumnType("numeric(38, 4)");

                entity.Property(e => e.SubTotalDiscount).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.TableId).HasColumnName("TableID");

                entity.Property(e => e.TableName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tax1).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.Tax2).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.Tax3).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.Total).HasColumnType("numeric(38, 4)");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.VendorId).HasColumnName("VendorID");

                entity.Property(e => e.VendorName)
                    .IsRequired()
                    .HasMaxLength(511)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewBill1>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewBills");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.AnnulledDate).HasColumnType("datetime");

                entity.Property(e => e.BillDate).HasColumnType("datetime");

                entity.Property(e => e.BillDiscount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.BillSubTotal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.BillTax).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.BillTax2).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.BillTips).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.BillTotal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CashName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CashRegisterId).HasColumnName("CashRegisterID");

                entity.Property(e => e.ClientName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Comment)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreditPaymentDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerDiscount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("EMail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SendCost).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.SessionId).HasColumnName("SessionID");

                entity.Property(e => e.StationId).HasColumnName("StationID");

                entity.Property(e => e.StationName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.TableId).HasColumnName("TableID");

                entity.Property(e => e.TableName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TotalExtra).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UserName)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewBillDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewBillDetail");

                entity.Property(e => e.AdultQuantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AmountByService).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.BillDiscount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CertificadoCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CertificadoId).HasColumnName("CertificadoID");

                entity.Property(e => e.ChildQuantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CommissionVendorPercent).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CustomerDiscount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DiscountCardAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DiscountTotal).HasColumnType("numeric(21, 2)");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.GiftTableId).HasColumnName("GiftTableID");

                entity.Property(e => e.GiftTableName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.MenuCategoryName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MenuSubCategoryName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Number)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PackageId).HasColumnName("PackageID");

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PriceAdultExtra).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PriceChildrenExtra).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ProductCategoryName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ProductDiscount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ProductMarcaName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProrrateoNc)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("ProrrateoNC");

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.QuantityAdultExtra).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.QuantityByRate).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.QuantityChildrenExtra).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.QuantityHours).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.RateId).HasColumnName("RateID");

                entity.Property(e => e.RateName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RatePrice).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.RemissionId).HasColumnName("RemissionID");

                entity.Property(e => e.RoomTypeName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ServiceCategoryName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ServiceSubCategoryName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StarDate).HasColumnType("datetime");

                entity.Property(e => e.SubTotal).HasColumnType("numeric(37, 4)");

                entity.Property(e => e.TableName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tax1).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tax2).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tax3).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.TaxTotal).HasColumnType("numeric(20, 2)");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Total).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.TrademarkId).HasColumnName("TrademarkID");

                entity.Property(e => e.UnitName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VendorId).HasColumnName("VendorID");

                entity.Property(e => e.VendorName)
                    .HasMaxLength(511)
                    .IsUnicode(false);

                entity.Property(e => e.WorkAreaId).HasColumnName("WorkAreaID");
            });

            modelBuilder.Entity<ViewBillRefund>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewBillRefund");

                entity.Property(e => e.AmountByService).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.BillDate).HasColumnType("datetime");

                entity.Property(e => e.BillId).HasColumnName("BillID");

                entity.Property(e => e.BillRefundDate).HasColumnType("datetime");

                entity.Property(e => e.Discount).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.IdbillRefund).HasColumnName("IDBillRefund");

                entity.Property(e => e.NameClient)
                    .HasMaxLength(511)
                    .IsUnicode(false);

                entity.Property(e => e.NumberBill)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SubTotal).HasColumnType("numeric(38, 4)");

                entity.Property(e => e.Tax1).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.Tax2).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.Total).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewBillRefundDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewBillRefundDetail");

                entity.Property(e => e.AdultQuantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AmountByService).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CertificadoCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CertificadoId).HasColumnName("CertificadoID");

                entity.Property(e => e.ChildQuantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Discount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.IdbillRefund).HasColumnName("IDBillRefund");

                entity.Property(e => e.IdbillRefundDetail).HasColumnName("IDBillRefundDetail");

                entity.Property(e => e.Number)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PackageId).HasColumnName("PackageID");

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PriceAdultExtra).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PriceChildrenExtra).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.QuantityAdultExtra).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.QuantityByRate).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.QuantityChildrenExtra).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.QuantityRefund).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.QuantityTmp).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.RateId).HasColumnName("RateID");

                entity.Property(e => e.RateName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RatePrice).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.StarDate).HasColumnType("datetime");

                entity.Property(e => e.SubTotal).HasColumnType("numeric(37, 4)");

                entity.Property(e => e.Tax1).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tax2).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.TaxTotal).HasColumnType("numeric(19, 2)");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Total).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.VendorId).HasColumnName("VendorID");

                entity.Property(e => e.VendorName)
                    .HasMaxLength(511)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewBillingAccountStatus>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewBillingAccountStatus");

                entity.Property(e => e.AfterBalancePayment).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.BillId).HasColumnName("BillID");

                entity.Property(e => e.CreditPaymentDate).HasColumnType("datetime");

                entity.Property(e => e.NumberReceipt)
                    .HasMaxLength(53)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentAmount).HasColumnType("numeric(38, 4)");

                entity.Property(e => e.PreviousBalancePayment).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.RegistrationDate).HasColumnType("datetime");

                entity.Property(e => e.RowId)
                    .HasMaxLength(84)
                    .IsUnicode(false)
                    .HasColumnName("RowID");
            });

            modelBuilder.Entity<ViewBreakdownCloseCash>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewBreakdownCloseCash");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.NamePaymentType)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OpeningCashId).HasColumnName("OpeningCashID");

                entity.Property(e => e.TotalPrimaryPaid).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.TotalSecondaryPaid).HasColumnType("numeric(38, 2)");
            });

            modelBuilder.Entity<ViewCashRegister>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewCashRegisters");

                entity.Property(e => e.AccessCode)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Comment)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.WareHouseName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewCashRegisterRole>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewCashRegisterRoles");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RolName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewClient>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewClients");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Balance).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Credit).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("EMail");

                entity.Property(e => e.Fax)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewClientsStruct>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_Clients_Struct");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Balance).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.Credit).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.DiscountValue).HasColumnType("numeric(9, 2)");

                entity.Property(e => e.DocumentId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DocumentID");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("EMail");

                entity.Property(e => e.Fax)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RecordDate).HasColumnType("datetime");

                entity.Property(e => e.Specialty)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.VendorId).HasColumnName("VendorID");
            });

            modelBuilder.Entity<ViewCloseCash>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewCloseCash");

                entity.Property(e => e.AmountByService).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.AmountByServiceSec).HasColumnType("numeric(38, 6)");

                entity.Property(e => e.Discount).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.DiscountSec).HasColumnType("numeric(38, 6)");

                entity.Property(e => e.EndDate).HasColumnType("smalldatetime");

                entity.Property(e => e.ExchangeMoney).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.ExchangeMoneySec).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MainCashFund).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.MaxBill)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MinBill)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NameCashRegister)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NameMainCurrency)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NameSecondaryCurrency)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NameUserF)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.NameUserI)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Rate).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.SecondaryCashFund).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.SimbolMainCurrency)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.SimbolSecondaryCurrency)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("smalldatetime");

                entity.Property(e => e.SubTotal).HasColumnType("numeric(38, 4)");

                entity.Property(e => e.SubTotalSec).HasColumnType("numeric(38, 6)");

                entity.Property(e => e.Tax1).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.Tax1Sec).HasColumnType("numeric(38, 6)");

                entity.Property(e => e.Tax2).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.Tax2Sec).HasColumnType("numeric(38, 6)");

                entity.Property(e => e.Tax3).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.Tax3Sec).HasColumnType("numeric(38, 6)");

                entity.Property(e => e.Total).HasColumnType("numeric(38, 4)");

                entity.Property(e => e.TotalContado).HasColumnType("numeric(38, 4)");

                entity.Property(e => e.TotalContadoSec).HasColumnType("numeric(38, 6)");

                entity.Property(e => e.TotalCredit).HasColumnType("decimal(38, 2)");

                entity.Property(e => e.TotalCreditSec).HasColumnType("numeric(38, 6)");

                entity.Property(e => e.TotalInCashRegisterPri).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.TotalInCashRegisterSec).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.TotalPrimaryPaid).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.TotalSec).HasColumnType("numeric(38, 6)");

                entity.Property(e => e.TotalSecondaryPaid).HasColumnType("numeric(38, 2)");
            });

            modelBuilder.Entity<ViewCorrectionWarehouse>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewCorrectionWarehouse");

                entity.Property(e => e.Comment).IsUnicode(false);

                entity.Property(e => e.CorrectionDate).HasColumnType("datetime");

                entity.Property(e => e.Reference).HasMaxLength(50);

                entity.Property(e => e.WarehouseName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewCorrectionWarehouseDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewCorrectionWarehouseDetail");

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Existence).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Input).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Output).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.QuantityReal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UnitName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewCreditBill>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewCreditBills");

                entity.Property(e => e.AddressClient)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.AmountByService).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.AnnulledDate).HasColumnType("datetime");

                entity.Property(e => e.AnnulledUserName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.BillDate).HasColumnType("datetime");

                entity.Property(e => e.CodeClient)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Comment)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreditPaymentDate).HasColumnType("datetime");

                entity.Property(e => e.Dias).HasColumnName("dias");

                entity.Property(e => e.Discount).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.DocumentIdclient)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DocumentIDClient");

                entity.Property(e => e.ExchangeRate).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.NameClient)
                    .HasMaxLength(511)
                    .IsUnicode(false);

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PaidBalance).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.PaymentAmount).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.PhoneClient)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RegionId).HasColumnName("RegionID");

                entity.Property(e => e.RegionName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.SubTotal).HasColumnType("numeric(38, 4)");

                entity.Property(e => e.Tax1).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.Tax2).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.Tax3).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.Total).HasColumnType("numeric(38, 4)");

                entity.Property(e => e.UnpaidBalance).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.VendorId).HasColumnName("VendorID");

                entity.Property(e => e.VendorName)
                    .IsRequired()
                    .HasMaxLength(511)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewCreditBillBalance>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_CreditBill_Balance");

                entity.Property(e => e.BillId).HasColumnName("BillID");

                entity.Property(e => e.UnpaidBalance).HasColumnType("numeric(38, 2)");
            });

            modelBuilder.Entity<ViewCreditNote>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewCreditNote");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.AnnulledDate).HasColumnType("datetime");

                entity.Property(e => e.Balance).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Comment)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.NameClient)
                    .HasMaxLength(511)
                    .IsUnicode(false);

                entity.Property(e => e.NameUser)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.NameUserAnnuller)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewCreditNoteInfo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewCreditNoteInfo");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.AnnulledDate).HasColumnType("datetime");

                entity.Property(e => e.Balance).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Comment)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CurrencySimbol)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.NameClient)
                    .HasMaxLength(511)
                    .IsUnicode(false);

                entity.Property(e => e.NameUser)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.NameUserAnnuller)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.RealCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewCreditNoteInfoDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewCreditNoteInfoDetail");

                entity.Property(e => e.Amount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CurrencySimbol)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.NumberInfoDetail)
                    .HasMaxLength(53)
                    .IsUnicode(false);

                entity.Property(e => e.OpeningCashId).HasColumnName("OpeningCashID");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.SessionId).HasColumnName("SessionID");
            });

            modelBuilder.Entity<ViewCreditsUnpaidDate>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewCreditsUnpaidDates");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreditPaymentDate).HasColumnType("datetime");

                entity.Property(e => e.Dias).HasColumnName("dias");

                entity.Property(e => e.DocumentId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DocumentID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.NameClient)
                    .HasMaxLength(511)
                    .IsUnicode(false);

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UnpaidBalance).HasColumnType("numeric(38, 2)");
            });

            modelBuilder.Entity<ViewDebitInvoice>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewDebitInvoice");

                entity.Property(e => e.CreditAmount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CurrencyName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CurrencySimbol)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.DateEndCredit).HasColumnType("datetime");

                entity.Property(e => e.ExchangeRate).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.InvoiceDate).HasColumnType("datetime");

                entity.Property(e => e.PrimaryCurrencyName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PrimaryCurrencySimbol)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Reference)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SupplyerName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UnpaidBalance).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.WarehouseName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewGeneralTable>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewGeneralTables");

                entity.Property(e => e.CashRegisterName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Comment)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GameName).HasMaxLength(50);

                entity.Property(e => e.GamePrice).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.GamePriceName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Lenght).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PriceHour).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PriceMinute).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.TableTypeName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Width).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Xpos).HasColumnName("XPos");

                entity.Property(e => e.Ypos).HasColumnName("YPos");
            });

            modelBuilder.Entity<ViewGoal>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewGoal");

                entity.Property(e => e.GoalId).HasColumnName("GoalID");

                entity.Property(e => e.MenuGoal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PercentMenu).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PercentProduct).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PercentRoom).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PercentService).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ProductGoal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ProductReal).HasColumnType("numeric(38, 4)");

                entity.Property(e => e.RoomGoal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ServiceGoal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ServiceReal).HasColumnType("numeric(38, 4)");

                entity.Property(e => e.TotalGoal).HasColumnType("numeric(21, 2)");

                entity.Property(e => e.TotalPercent).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalReal).HasColumnType("numeric(38, 4)");

                entity.Property(e => e.VendorId).HasColumnName("VendorID");

                entity.Property(e => e.VendorName)
                    .HasMaxLength(511)
                    .IsUnicode(false);

                entity.Property(e => e.WorkAreaId).HasColumnName("WorkAreaID");

                entity.Property(e => e.WorkAreaName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewGoalRegion>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewGoalRegion");

                entity.Property(e => e.GoalId).HasColumnName("GoalID");

                entity.Property(e => e.MenuGoal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PercentMenu).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PercentProduct).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PercentRoom).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PercentService).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ProductGoal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ProductReal).HasColumnType("numeric(38, 4)");

                entity.Property(e => e.RegionId).HasColumnName("RegionID");

                entity.Property(e => e.RegionName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.RoomGoal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ServiceGoal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ServiceReal).HasColumnType("numeric(38, 4)");

                entity.Property(e => e.TotalGoal).HasColumnType("numeric(21, 2)");

                entity.Property(e => e.TotalPercent).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalReal).HasColumnType("numeric(38, 4)");

                entity.Property(e => e.VendorId).HasColumnName("VendorID");

                entity.Property(e => e.VendorName)
                    .HasMaxLength(511)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewInformePosInventario>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_informe_pos_inventario");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CantInv).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Cantidad)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Cedula)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Ciudad)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CodPostal)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Comentarios)
                    .IsRequired()
                    .HasMaxLength(18)
                    .IsUnicode(false);

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Departamento)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha).HasMaxLength(4000);

                entity.Property(e => e.NameClient)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.NameMoneda)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.NameProduct)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NameUnit)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NombreDist)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Pais)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.ProductCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ProductMarcaName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TrademarkId).HasColumnName("TrademarkID");
            });

            modelBuilder.Entity<ViewInformePosVenta>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_informe_pos_ventas");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CantInv)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Cantidad).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Cedula)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Ciudad)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CodPostal)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Comentarios)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Departamento)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.NameClient)
                    .HasMaxLength(511)
                    .IsUnicode(false);

                entity.Property(e => e.NameMoneda)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NameProduct)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.NameUnit)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NombreDist)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Pais)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ProductCode)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ProductMarcaName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TrademarkId).HasColumnName("TrademarkID");
            });

            modelBuilder.Entity<ViewInvoice>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewInvoice");

                entity.Property(e => e.CreditAmount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CurrencyName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CurrencySimbol)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.DateEndCredit).HasColumnType("datetime");

                entity.Property(e => e.ExchangeRate).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.InvoiceDate).HasColumnType("datetime");

                entity.Property(e => e.PrimaryCurrencyName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PrimaryCurrencySimbol)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Reference)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SupplyerName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.WarehouseName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewInvoiceDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewInvoiceDetail");

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CostCp).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.NameTax)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.SubTotal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.SubTotalCp).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.TaxPercent).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.TaxValue).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.TaxValueCp).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UnitName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewKadex>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_Kadex");

                entity.Property(e => e.CodeProduct)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CostFinal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CostInitial).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CostUnitActual).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CostUnitOperation).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CostoUnitFinal).HasColumnType("numeric(38, 20)");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Input).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.NameProduct)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NameUnit)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Output).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.QuantityFinal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.QuantityInitial).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ReferenceDocument)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ReferenceDocumentId).HasColumnName("ReferenceDocumentID");
            });

            modelBuilder.Entity<ViewListProduct>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_List_Product");

                entity.Property(e => e.BarCode)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NameCategoria)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Quantity).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.StrMarca)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("strMarca");

                entity.Property(e => e.Unit)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewListProduct1>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewListProduct");

                entity.Property(e => e.AlertQuantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.BarCode)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Comment)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Discount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Portion).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PricePortion).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ProductCategoryName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.SubUnitName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UnitName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewListProductByWarehouseFastBilling>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewListProductByWarehouseFastBilling");

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Location)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Presentation)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ProductCategoryName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.TrademarkName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Treatment)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UnitName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewListRemision>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewListRemision");

                entity.Property(e => e.AddressClient)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.AnnulledDate).HasColumnType("datetime");

                entity.Property(e => e.Client)
                    .HasMaxLength(511)
                    .IsUnicode(false);

                entity.Property(e => e.CodeClient)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DocumentIdclient)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DocumentIDClient");

                entity.Property(e => e.DocumentTypeClient)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneClient)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RegionId).HasColumnName("RegionID");

                entity.Property(e => e.RegionName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.TotalAmount).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.TotalBilled).HasColumnType("numeric(38, 4)");

                entity.Property(e => e.TotalSaldo).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.Vendor)
                    .HasMaxLength(511)
                    .IsUnicode(false);

                entity.Property(e => e.WarehouseId).HasColumnName("WarehouseID");
            });

            modelBuilder.Entity<ViewListWithholding>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_ListWithholding");

                entity.Property(e => e.Amount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.BillNumberWithholding)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ExchangeRate).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.NumberWithholding)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OpeningCashId).HasColumnName("OpeningCashID");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PorcWithholding).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.PrimaryAmount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.SessionId).HasColumnName("SessionID");

                entity.Property(e => e.Simbol1)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Simbol2)
                    .HasMaxLength(3)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewMarginsMenuItem>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewMarginsMenuItem");

                entity.Property(e => e.BillDate).HasColumnType("datetime");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Cost).HasColumnType("numeric(37, 4)");

                entity.Property(e => e.Margins).HasColumnType("numeric(38, 4)");

                entity.Property(e => e.MenuName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NameCategory)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NameSubCategory)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SubCategoryId).HasColumnName("SubCategoryID");

                entity.Property(e => e.SubTotalSales).HasColumnType("numeric(38, 4)");
            });

            modelBuilder.Entity<ViewMarginsProduct>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewMarginsProduct");

                entity.Property(e => e.BillDate).HasColumnType("datetime");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Cost).HasColumnType("numeric(37, 4)");

                entity.Property(e => e.Margins).HasColumnType("numeric(38, 4)");

                entity.Property(e => e.NameCategory)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.NameTrademark)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SubTotalSales).HasColumnType("numeric(38, 4)");

                entity.Property(e => e.TrademarkId).HasColumnName("TrademarkID");
            });

            modelBuilder.Entity<ViewMarginsRoom>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewMarginsRoom");

                entity.Property(e => e.BillDate).HasColumnType("datetime");

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Margins).HasColumnType("numeric(38, 4)");

                entity.Property(e => e.NameRoomType)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RoomName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SubTotalSales).HasColumnType("numeric(38, 4)");
            });

            modelBuilder.Entity<ViewMarginsService>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewMarginsService");

                entity.Property(e => e.BillDate).HasColumnType("datetime");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Cost).HasColumnType("numeric(37, 4)");

                entity.Property(e => e.Margins).HasColumnType("numeric(38, 4)");

                entity.Property(e => e.NameCategory)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NameSubCategory)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ServiceName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SubCategoryId).HasColumnName("SubCategoryID");

                entity.Property(e => e.SubTotalSales).HasColumnType("numeric(38, 4)");
            });

            modelBuilder.Entity<ViewMenu>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewMenus");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CommandId).HasColumnName("CommandID");

                entity.Property(e => e.CommandName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ElaborationCost).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IsOpenBar).HasColumnName("isOpenBar");

                entity.Property(e => e.MenuItemCategoryId).HasColumnName("MenuItemCategoryID");

                entity.Property(e => e.MenuItemSubCategoryId).HasColumnName("MenuItemSubCategoryID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PriceHappy).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PromotionCommission).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.QtyMin).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.SpecialDrinkPrices).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.SubCategoryName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TotalCost).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<ViewMenuAtribb>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewMenuAtribb");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.PromotionCommission).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.SpecialDrinkPrices).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<ViewOrder>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewOrder");

                entity.Property(e => e.AnnulledDate).HasColumnType("datetime");

                entity.Property(e => e.CreditPaymentDate).HasColumnType("datetime");

                entity.Property(e => e.NameClient)
                    .HasMaxLength(511)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.UserNameCreditPayment)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewOrderDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewOrderDetail");

                entity.Property(e => e.AdultQuantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AmountByService).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ChildQuantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CustomerDiscount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DiscountTotal).HasColumnType("numeric(19, 2)");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.MenuCategoryName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MenuSubCategoryName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Number)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PackageId).HasColumnName("PackageID");

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PriceAdultExtra).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PriceChildrenExtra).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ProductCategoryName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ProductDiscount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ProductMarcaName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.QuantityAdultExtra).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.QuantityByRate).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.QuantityChildrenExtra).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.QuantityHours).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.RateId).HasColumnName("RateID");

                entity.Property(e => e.RateName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RatePrice).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.RoomTypeName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ServiceCategoryName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ServiceSubCategoryName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StarDate).HasColumnType("datetime");

                entity.Property(e => e.SubTotal).HasColumnType("numeric(37, 4)");

                entity.Property(e => e.Tax1).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tax2).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tax3).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.TaxTotal).HasColumnType("numeric(20, 2)");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Total).HasColumnType("numeric(38, 4)");

                entity.Property(e => e.VendorId).HasColumnName("VendorID");

                entity.Property(e => e.VendorName)
                    .HasMaxLength(511)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewPayment>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewPayment");

                entity.Property(e => e.AnnulledDate).HasColumnType("datetime");

                entity.Property(e => e.NameCashRegister)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OpeningCashId).HasColumnName("OpeningCashID");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.SessionId).HasColumnName("SessionID");
            });

            modelBuilder.Entity<ViewPaymentDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewPaymentDetail");

                entity.Property(e => e.Amount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AuthorizationNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Bank)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BankAccount)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CertificateRealCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CheckNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreditCardExpirationDate)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreditCardNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CurrencyName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ExchangeRate).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NumberTransfer)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrimaryAmount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PrimaryCurrencyName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Simbol)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.TransferDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ViewProductInOut>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewProductInOut");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Observation)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ReferenceName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ReferenceNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Total).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.WarehouseName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewProductInOutDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewProductInOutDetail");

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.IdproductInOut).HasColumnName("IDProductInOut");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.SubTotal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UnitName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewProductSupplier>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewProductSupplier");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PrimaryUnit)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SecondaryUnit)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            });

            modelBuilder.Entity<ViewProform>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewProform");

                entity.Property(e => e.AmountByService).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.AnnulledDate).HasColumnType("datetime");

                entity.Property(e => e.Comment)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Discount).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.DiscountPercent).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.NameClient)
                    .HasMaxLength(511)
                    .IsUnicode(false);

                entity.Property(e => e.OpeningDate).HasColumnType("datetime");

                entity.Property(e => e.PrimarySimbol)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.SecondarySimbol)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.SpecialDiscount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.SubTotal).HasColumnType("numeric(38, 4)");

                entity.Property(e => e.Tax1).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.Tax2).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.Tax3).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.Total).HasColumnType("numeric(38, 4)");

                entity.Property(e => e.TotalTax).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewProformDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewProformDetail");

                entity.Property(e => e.AdultQuantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AmountByService).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ChildQuantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CustomerDiscount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DiscountTotal).HasColumnType("numeric(19, 2)");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.MenuCategoryName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MenuSubCategoryName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Number)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PackageId).HasColumnName("PackageID");

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PriceAdultExtra).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PriceChildrenExtra).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ProductCategoryName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ProductDiscount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ProductMarcaName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.QuantityAdultExtra).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.QuantityByRate).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.QuantityChildrenExtra).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.QuantityHours).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.RateId).HasColumnName("RateID");

                entity.Property(e => e.RateName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RatePrice).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.RoomTypeName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ServiceCategoryName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ServiceSubCategoryName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StarDate).HasColumnType("datetime");

                entity.Property(e => e.SubTotal).HasColumnType("numeric(37, 4)");

                entity.Property(e => e.Tax1).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tax2).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tax3).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.TaxTotal).HasColumnType("numeric(20, 2)");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Total).HasColumnType("numeric(38, 4)");

                entity.Property(e => e.UnitName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VendorId).HasColumnName("VendorID");

                entity.Property(e => e.VendorName)
                    .HasMaxLength(511)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewReceipt>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewReceipt");

                entity.Property(e => e.AfterBalance).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AnnulledDate).HasColumnType("datetime");

                entity.Property(e => e.AnnulledUserName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CashRegisterId).HasColumnName("CashRegisterID");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Concept)
                    .HasMaxLength(757)
                    .IsUnicode(false);

                entity.Property(e => e.ExchangeRate).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Idcliente).HasColumnName("IDCliente");

                entity.Property(e => e.NamePc)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NamePC");

                entity.Property(e => e.NameSc)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NameSC");

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OpeningCashId).HasColumnName("OpeningCashID");

                entity.Property(e => e.PreviousBalance).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Reference)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RegistrationDate).HasColumnType("datetime");

                entity.Property(e => e.SessionId).HasColumnName("SessionID");

                entity.Property(e => e.SimbolPc)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("SimbolPC");

                entity.Property(e => e.SimbolPsc)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("SimbolPSC");

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.TotalWithholding).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewRecoveryBilling>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_RecoveryBilling");

                entity.Property(e => e.BillBumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BillDate).HasColumnType("datetime");

                entity.Property(e => e.NameClient)
                    .HasMaxLength(511)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentAmount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ReceiptDate).HasColumnType("datetime");

                entity.Property(e => e.ReceiptId).HasColumnName("ReceiptID");

                entity.Property(e => e.ReceiptNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RegionId).HasColumnName("RegionID");

                entity.Property(e => e.RegionName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.VendorId).HasColumnName("VendorID");

                entity.Property(e => e.VendorName)
                    .HasMaxLength(511)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewRemission>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewRemission");

                entity.Property(e => e.AnnulledDate).HasColumnType("datetime");

                entity.Property(e => e.ClientAddress)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ClientPhone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NameClient)
                    .HasMaxLength(511)
                    .IsUnicode(false);

                entity.Property(e => e.NameVendor)
                    .HasMaxLength(511)
                    .IsUnicode(false);

                entity.Property(e => e.Observation)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.RemisionDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ViewRemissionDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewRemissionDetail");

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PriceTotal).HasColumnType("numeric(38, 6)");

                entity.Property(e => e.ProductCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.SubTotal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tax).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.TaxPorc).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.Total).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<ViewRepInvoicedetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_rep_invoicedetail");

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.InvoiceDate).HasColumnType("datetime");

                entity.Property(e => e.NameTax)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Reference)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SubTotal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.SupplyerName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TaxValue).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UnitName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.WarehouseName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewRepaymentDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_RepaymentDetail");

                entity.Property(e => e.AfterBalancePayment).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.BillId).HasColumnName("BillID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.NumberBill)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentAmount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PreviousBalancePayment).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ReceiptId).HasColumnName("ReceiptID");

                entity.Property(e => e.Retencion1).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.Retencion2).HasColumnType("numeric(38, 2)");
            });

            modelBuilder.Entity<ViewSupplier>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewSupplier");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Comment)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Contact)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewTableReservation>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewTableReservations");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TableId).HasColumnName("TableID");

                entity.Property(e => e.TableName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewTotalDetailByBill>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_TotalDetailByBill");

                entity.Property(e => e.AmountByService).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.CostTotal).HasColumnType("numeric(38, 4)");

                entity.Property(e => e.Discount).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.SubTotal).HasColumnType("numeric(38, 4)");

                entity.Property(e => e.SubTotalAfterDiscount)
                    .HasColumnType("numeric(38, 4)")
                    .HasColumnName("SubTotal_After_Discount");

                entity.Property(e => e.SubTotalE)
                    .HasColumnType("numeric(38, 4)")
                    .HasColumnName("SubTotal_E");

                entity.Property(e => e.SubTotalEAfterDiscount)
                    .HasColumnType("numeric(38, 4)")
                    .HasColumnName("SubTotal_E_After_Discount");

                entity.Property(e => e.SubTotalG)
                    .HasColumnType("numeric(38, 4)")
                    .HasColumnName("SubTotal_G");

                entity.Property(e => e.SubTotalGAfterDiscount)
                    .HasColumnType("numeric(38, 4)")
                    .HasColumnName("SubTotal_G_After_Discount");

                entity.Property(e => e.SubTotalProductE)
                    .HasColumnType("numeric(38, 4)")
                    .HasColumnName("SubTotalProduct_E");

                entity.Property(e => e.SubTotalProductG)
                    .HasColumnType("numeric(38, 4)")
                    .HasColumnName("SubTotalProduct_G");

                entity.Property(e => e.SubTotalServiceE)
                    .HasColumnType("numeric(38, 4)")
                    .HasColumnName("SubTotalService_E");

                entity.Property(e => e.SubTotalServiceG)
                    .HasColumnType("numeric(38, 4)")
                    .HasColumnName("SubTotalService_G");

                entity.Property(e => e.Tax1).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.Tax2).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.Tax3).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.Total).HasColumnType("numeric(38, 4)");

                entity.Property(e => e.TotalTax).HasColumnType("numeric(38, 2)");
            });

            modelBuilder.Entity<ViewTotalPaymentReceiptByBill>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_Total_Payment_Receipt_By_Bill");

                entity.Property(e => e.BillId).HasColumnName("BillID");

                entity.Property(e => e.PaymentAmount).HasColumnType("numeric(38, 2)");
            });

            modelBuilder.Entity<ViewTotalRefoundByBill>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_TotalRefoundByBill");

                entity.Property(e => e.AmountByService).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.BillId).HasColumnName("BillID");

                entity.Property(e => e.CostTotal).HasColumnType("numeric(38, 4)");

                entity.Property(e => e.Discount).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.SubTotal).HasColumnType("numeric(38, 4)");

                entity.Property(e => e.SubTotalAfterDiscount)
                    .HasColumnType("numeric(38, 4)")
                    .HasColumnName("SubTotal_After_Discount");

                entity.Property(e => e.SubTotalE)
                    .HasColumnType("numeric(38, 4)")
                    .HasColumnName("SubTotal_E");

                entity.Property(e => e.SubTotalEAfterDiscount)
                    .HasColumnType("numeric(38, 4)")
                    .HasColumnName("SubTotal_E_After_Discount");

                entity.Property(e => e.SubTotalG)
                    .HasColumnType("numeric(38, 4)")
                    .HasColumnName("SubTotal_G");

                entity.Property(e => e.SubTotalGAfterDiscount)
                    .HasColumnType("numeric(38, 4)")
                    .HasColumnName("SubTotal_G_After_Discount");

                entity.Property(e => e.SubTotalProductE)
                    .HasColumnType("numeric(38, 4)")
                    .HasColumnName("SubTotalProduct_E");

                entity.Property(e => e.SubTotalProductG)
                    .HasColumnType("numeric(38, 4)")
                    .HasColumnName("SubTotalProduct_G");

                entity.Property(e => e.SubTotalServiceE)
                    .HasColumnType("numeric(38, 4)")
                    .HasColumnName("SubTotalService_E");

                entity.Property(e => e.SubTotalServiceG)
                    .HasColumnType("numeric(38, 4)")
                    .HasColumnName("SubTotalService_G");

                entity.Property(e => e.Tax1).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.Tax2).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.Total).HasColumnType("numeric(38, 4)");

                entity.Property(e => e.TotalTax).HasColumnType("numeric(38, 2)");
            });

            modelBuilder.Entity<ViewUser>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewUsers");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RolName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewWaiterPoint>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewWaiterPoints");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ParentName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewWarehouseProduct>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewWarehouseProduct");

                entity.Property(e => e.AlertQuantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.BarCode)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Comment)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ConteoMinimo)
                    .IsRequired()
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Conteo_Minimo");

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Discount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Portion).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PricePortion).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ProductCategoryName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Qty).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.QtyTotal)
                    .HasMaxLength(81)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.QuantityBill).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.SubUnitName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SubUnitQty).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.TotalCost).HasColumnType("numeric(37, 4)");

                entity.Property(e => e.UnitName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.WarehouseName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewWarehouseTranfer>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewWarehouseTranfer");

                entity.Property(e => e.Comment).IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.FromWarehouseName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Reference)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ToWarehouseName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewWarehouseTranferDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewWarehouseTranferDetail");

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Existence).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.IdwareHouseTranfer).HasColumnName("IDWareHouseTranfer");

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UnitName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewWorkOrder>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewWorkOrder");

                entity.Property(e => e.AgentId).HasColumnName("AgentID");

                entity.Property(e => e.AgentName)
                    .HasMaxLength(511)
                    .IsUnicode(false);

                entity.Property(e => e.AnnulledDate).HasColumnType("datetime");

                entity.Property(e => e.AppliedDate).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Observation)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ReferenceName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Subtotal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tax).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Total).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UserName)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewWorkOrderDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewWorkOrderDetail");

                entity.Property(e => e.AgentId).HasColumnName("AgentID");

                entity.Property(e => e.AgentName)
                    .HasMaxLength(511)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.RequisitionId).HasColumnName("RequisitionID");

                entity.Property(e => e.SubTotal).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<ViewWorkOrderProductDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewWorkOrderProductDetail");

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.SubTotal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tax1).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UnitId).HasColumnName("UnitID");

                entity.Property(e => e.UnitName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.WorkOrderDetailId).HasColumnName("WorkOrderDetailID");
            });

            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.ToTable("Warehouse");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<WarehouseProduct>(entity =>
            {
                entity.ToTable("WarehouseProduct");

                entity.HasIndex(e => e.Product, "IX_WP_Product");

                entity.HasIndex(e => e.Warehouse, "IX_WP_Warehouse");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.SubUnitQty)
                    .HasColumnType("numeric(18, 2)")
                    .HasComputedColumnSql("([dbo].[GetQuantitySubUnitByProduct]([Product],[Warehouse]))", false);
            });

            modelBuilder.Entity<WarehouseTranfer>(entity =>
            {
                entity.ToTable("WarehouseTranfer");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Comment).IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Reference)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<WarehouseTranferDetail>(entity =>
            {
                entity.ToTable("WarehouseTranferDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Existence).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<Withholding>(entity =>
            {
                entity.ToTable("Withholding");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Comment)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.PorcAmount).HasColumnType("numeric(18, 4)");
            });

            modelBuilder.Entity<WorkArea>(entity =>
            {
                entity.ToTable("WorkArea");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<WorkOrder>(entity =>
            {
                entity.ToTable("WorkOrder");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AgentId).HasColumnName("AgentID");

                entity.Property(e => e.AnnulledDate).HasColumnType("datetime");

                entity.Property(e => e.AppliedDate).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Observation)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ReferenceName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Subtotal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tax).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Total).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<WorkOrderDetail>(entity =>
            {
                entity.ToTable("WorkOrderDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.RequisitionId).HasColumnName("RequisitionID");

                entity.Property(e => e.SubTotal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.WorkOrderId).HasColumnName("WorkOrderID");
            });

            modelBuilder.Entity<WorkOrderProductDetail>(entity =>
            {
                entity.ToTable("WorkOrderProductDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Quantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.SubTotal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tax1).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tax1Percent).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.WorkOrderDetailId).HasColumnName("WorkOrderDetailID");

                entity.Property(e => e.WorkOrderId).HasColumnName("WorkOrderID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
