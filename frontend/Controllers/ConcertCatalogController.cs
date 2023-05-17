using GloboTicket.Frontend.Extensions;
using GloboTicket.Frontend.Models;
using GloboTicket.Frontend.Models.View;
using GloboTicket.Frontend.Services;
using GloboTicket.Frontend.Services.ShoppingBasket;
using Microsoft.AspNetCore.Mvc;
using Prometheus;

namespace GloboTicket.Frontend.Controllers;

public class ConcertCatalogController : Controller
{
    private readonly IConcertCatalogService _concertCatalogService;
    private readonly IShoppingBasketService _shoppingBasketService;
    private readonly Settings _settings;
    private static ICounter? _concertCatalogHomePageVisits = null;
    private static ICounter? _concertCatalogDetailsPageVisits = null;

    public ConcertCatalogController(IConcertCatalogService concertCatalogService, IShoppingBasketService shoppingBasketService, Settings settings)
    {
        this._concertCatalogService = concertCatalogService;
        this._shoppingBasketService = shoppingBasketService;
        this._settings = settings;
    }

    public async Task<IActionResult> Index()
    {
        RegisterMetricsConcertCatalogHomePageVisits();
        var currentBasketId = Request.Cookies.GetCurrentBasketId(_settings);

        var getBasket = _shoppingBasketService.GetBasket(currentBasketId);
        var getConcerts = _concertCatalogService.GetAll();

        await Task.WhenAll(new Task[] { getBasket, getConcerts });

        var numberOfItems = getBasket.Result.NumberOfItems;

        return View(
            new ConcertListModel
            {
                Concerts = getConcerts.Result,
                NumberOfItems = numberOfItems,
            }
        );
    }

    private void RegisterMetricsConcertCatalogHomePageVisits()
    {
        _concertCatalogHomePageVisits ??= Metrics.CreateCounter("globoticket_frontend_home_page_visits", "Number of Frontend Concerts Home page visited");
        _concertCatalogHomePageVisits.Inc();
    }

    public async Task<IActionResult> Detail(Guid concertId)
    {
        RegisterMetricsConcertCatalogDetailsPageVisits();
        var ev = await _concertCatalogService.GetConcert(concertId);
        return View(ev);
    }

    private void RegisterMetricsConcertCatalogDetailsPageVisits()
    {
        _concertCatalogDetailsPageVisits ??= Metrics.CreateCounter("globoticket_frontend_details_page_visits", "Number of Frontend Concert's Details page visited");
        _concertCatalogDetailsPageVisits.Inc();
    }

}
