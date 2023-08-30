class CartClick {

    clickIncrement(btn) {
        let data = this.getData(btn);
        data.Quantidade++
        this.postAmount(data);
    }

    clickDecrement(btn) {
        let data = this.getData(btn);
        data.Quantidade--
        this.postAmount(data);
    }

    updateAmount(input) {
        let data = this.getData(input);
        this.postAmount(data);
    }

    getData(element) {
        var itemLine = $(element).parents('[itemid]');
        var item = $(itemLine).attr('itemid');
        var newAmount = $(itemLine).find('input').val();

        return {
            Id: item,
            Quantidade: newAmount //it might be better a view model so i ain't change the accessibility
        };
    }

    postAmount(data) {

        let token = $('[name=__RequestVerificationToken]').val();
        let headers = {};
        headers['RequestVerificationToken'] = token;

        $.ajax({
            url: '/Order/UpdateAmountOfItems',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            headers: headers
        })
            .done(function (response) {
                let itemOrder = response.itemOrder;
                let itemLine = $('[itemid="' + itemOrder.id + '"]');
                itemLine.find('#amount').val(itemOrder.quantidade);

                itemLine.find('[subtotal]').html((itemOrder.subtotal).twoDecimals());

                let cartViewModel = response.carrinhoViewModel;

                $('[numero-itens]').html('Total: ' + cartViewModel.items.length + ' itens');

                $('[total]').html((cartViewModel.total).twoDecimals());

                if (itemOrder.quantidade == 0) {
                    itemLine.remove();
                }
            }); let token = $('[name=__RequestVerificationToken]').val();
        let headers = {};
        headers['RequestVerificationToken'] = token;

        $.ajax({
            url: '/Order/UpdateAmountOfItems',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            headers: headers
        })
            .done(function (response) {
                let itemOrder = response.itemOrder;
                let itemLine = $('[itemid="' + itemOrder.id + '"]');
                itemLine.find('#amount').val(itemOrder.quantidade);

                itemLine.find('[subtotal]').html((itemOrder.subtotal).twoDecimals());

                let cartViewModel = response.carrinhoViewModel;

                $('[numero-itens]').html('Total: ' + cartViewModel.items.length + ' itens');

                $('[total]').html((cartViewModel.total).twoDecimals());

                if (itemOrder.quantidade == 0) {
                    itemLine.remove();
                }
            });
    }
}

var carrinho = new CartClick();

Number.prototype.twoDecimals = function () {
    return this.toFixed(2).replace('.', ',');
}