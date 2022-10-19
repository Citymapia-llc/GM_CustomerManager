function RazorPay(model, compRef) {
    var options = {
        "key": model.razorpayKey,
        "amount": model.razorpayAmount,
        "currency": model.currency,
        "name": model.buyer_name,
        "description": model.orderNumber,
        "image": model.logo,
        "order_id": model.order_id,
        "handler": function (response) {
            compRef.invokeMethodAsync('razorpaysuccess', model.orderNumber, response.razorpay_payment_id, response.razorpay_order_id)
                .then(data => {
                    console.log(data);
                });
        },
        "modal": {
            // "confirm_close" : true,
            "ondismiss": function () {
                compRef.invokeMethodAsync('razorpaymodalclose', model.orderNumber)
                    .then(data => {
                        //$('.razorpay-container').css('display', 'none');
                    });
            }
        },
        "prefill": {
            "name": model.buyer_name,
            "email": model.email,
            "contact": model.phone
        },
        "notes": {
            "address": model.billing_address
        },
        "theme": {
            "color": "#3399cc"
        }
    };
    var rzp1 = new Razorpay(options);
    rzp1.on('payment.failed', function (response) {
        alert(response.error.code);
        alert(response.error.description);
        alert(response.error.source);
        alert(response.error.step);
        alert(response.error.reason);
        alert(response.error.metadata.order_id);
        alert(response.error.metadata.payment_id);
    });
    rzp1.open();
}