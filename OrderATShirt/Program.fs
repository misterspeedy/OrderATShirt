namespace OrderATShirt

// Download ChromeDriver from https://sites.google.com/a/chromium.org/chromedriver/downloads
// Copy the exe to "C:\Program Files (x86)\ChromeDriver" or another convenient location

module Main = 

    open System
    open canopy
    open runner
    open OpenQA.Selenium

    [<AutoOpen>]
    module Selectors =
        let womensShirt = """#page_productsSearch_collection_cell4"""
        let womensShirtHover = """#page_productsSearch_collection_cell4_hovered-preview"""
        let amountSelector = """#main > div > div.OffCanvasSliders.OffCanvasSliders--isNarrow > div.OffCanvasSliders-content > div > div.Pdp > div:nth-child(2) > div > div.TopBar > div > div.TopBar-right > div.TopBar-purchasePod > div.TopBar-quantityAddToCart > div > div > div > div > div > div > div > div > span.QuantitySelector-quantity"""
        let sizeSelector = """#main > div > div.OffCanvasSliders > div.OffCanvasSliders-content > div > div.Pdp > div:nth-child(2) > div > div.Product > div > div > div.ProductSpace-attributes > div > div.Attributes-level1 > div.Attributes-level1Group > div:nth-child(2) > div > div.Level1Droplist-right > div.Level1Droplist-droplistAndL2Link > div > div > div > div.Droplist-selectedItem > div > div"""
        let addToBasket = """#main > div > div.OffCanvasSliders.OffCanvasSliders--isNarrow > div.OffCanvasSliders-content > div > div.Pdp > div:nth-child(2) > div > div.TopBar > div > div.TopBar-right > div.TopBar-purchasePod > div.TopBar-quantityAddToCart > button"""
        let basketItem = """#page > div.row.page-checkoutRedesign > div > div.row.justAddedItem > div.large-7.small-12.column > div.justAddedItemTitle.itemTitle > p > a"""
        let proceedToCheckout = """#page_checkoutButton1-text"""

    module Initialize =
        canopy.configuration.chromeDir <- @"C:\Program Files (x86)\ChromeDriver"
        start chrome

    "I can order a t-shirt" &&& fun _ -> 

        // Go to zazzle:
        url @"https://www.zazzle.co.uk/fsharporg/gifts"

        // Select a t-shirt:
        waitForElement womensShirt

        hover womensShirt

        click womensShirtHover

        waitForElement amountSelector

        click amountSelector

        press Keys.Backspace
        press Keys.NumberPad2
        press Keys.Enter

        click sizeSelector
        
        press Keys.Down
        press Keys.Down

        // Add to basket:
        click addToBasket

        waitForElement proceedToCheckout

        // Assert there is one t-shirt in the recent basket items:
        count basketItem 1

    run()

    printfn "Press [Enter] to exit"
    Console.ReadKey() |> ignore
    
    quit()
