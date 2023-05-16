using GloboTicket.Ordering.Model;
using GloboTicket.Ordering.Services;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Mvc;
using Prometheus;

namespace GloboTicket.Ordering.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> logger;
    private readonly EmailSender emailSender;
    //private readonly TelemetryClient telemetryCient;
    private static ICounter? _ordersCompleted = null;

    //public OrderController(ILogger<OrderController> logger, TelemetryClient telemetryClient, EmailSender emailSender)
    public OrderController(ILogger<OrderController> logger, EmailSender emailSender)
    {
        this.logger = logger;
        this.emailSender = emailSender;
        //this.telemetryCient = telemetryClient;
    }

    [HttpPost("", Name = "SubmitOrder")]
    public IActionResult Submit(OrderForCreation order)
    {
        logger.LogInformation($"Received a new order from {order.CustomerDetails.Name}");
        RegisterMetricsOrderProcessed();
        //SendAppInsightsTelemetryOrderProcessed();
        emailSender.SendEmailForOrder(order);
        return Ok();
    }

    private void RegisterMetricsOrderProcessed()
    {
        _ordersCompleted ??= Metrics.CreateCounter("globoticket_orders_processed", "Number of orders completed");
        _ordersCompleted.Inc();
    }

    //private void SendAppInsightsTelemetryOrderProcessed()
    //{
    //    MetricTelemetry telemetry = new MetricTelemetry();
    //    telemetry.Name = "Order processed";

    //    telemetry.Count = 1 ;
    //    telemetryCient.TrackMetric(telemetry);
    //}
}
