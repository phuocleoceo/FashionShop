namespace back_end.Extension;

public static class ConstantValue
{
    public const string Role_Admin = "Admin";
    public const string Role_Customer = "Customer";

    public const string OrderStatusPending = "Pending";
    public const string OrderStatusApproved = "Approved";
    public const string OrderStatusInProcess = "Processing";
    public const string OrderStatusShipped = "Shipped";
    public const string OrderStatusCancelled = "Cancelled";
    public const string OrderStatusRefunded = "Refunded";

    public const string PaymentStatusPending = "Pending";
    public const string PaymentStatusApproved = "Approved";
    public const string PaymentStatusDelayedPayment = "ApprovedForDelayedPayment";
    public const string PaymentStatusRejected = "Rejected";
}