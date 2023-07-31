﻿function updateQuantityAndPriceUp(e) {
    e.preventDefault();
    let quantityBoxAndButtuns = e.currentTarget.parentNode;

    let price = quantityBoxAndButtuns.children[1];
    let quantity = quantityBoxAndButtuns.children[2];


    quantity.value = Number(quantity.value) + 1;

    let totalPrice = quantity.value * price.value;

    let bookInnerChildren = quantityBoxAndButtuns.parentNode;

    let bookTotalPrice = bookInnerChildren.children[3].children[0].children[0];

    bookTotalPrice.textContent = totalPrice.toFixed(2);

    let totalPriceShoppingCart = bookInnerChildren
        .parentNode
        .parentNode
        .parentNode
        .lastChild
        .previousSibling
        .firstElementChild
        .firstElementChild
        .firstElementChild
        .firstElementChild;


    totalPriceShoppingCart.textContent = (Number(totalPriceShoppingCart.textContent) + Number(price.value)).toFixed(2);
}
