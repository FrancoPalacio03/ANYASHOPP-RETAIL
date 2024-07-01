document.getElementById('sale-btn').addEventListener('click', function() {
    const tableBody = document.querySelector('#cart-details-body');
    
    tableBody.innerHTML = '';

    let subTotal = 0;
    let totalDiscount = 0;
    let itemCount = 0;

    cartProducts.forEach(item => {
        itemCount++;
        const productPrice = parseFloat(item.product.price);
        const productQuantity = parseInt(item.quantity);
        const productDiscount = parseFloat(item.product.discount || 0);

        const productTotal = productPrice * productQuantity;
        subTotal += productTotal;

        totalDiscount += (productDiscount * productPrice / 100) * productQuantity;

        const row = `
            <tr>
                <td>${itemCount.toString().padStart(2, '0')}</td>
                <td>${item.product.name}</td>
                <td>$${new Intl.NumberFormat('en-US', { maximumFractionDigits: 2 }).format(productPrice)}</td>
                <td>${productQuantity}</td>
                <td class="text-end">$${new Intl.NumberFormat('en-US', { maximumFractionDigits: 2 }).format(productTotal)}</td>
            </tr>
        `;
        tableBody.insertAdjacentHTML('beforeend', row);
    });

    const tax = parseFloat(subTotal * 0.21);
    const total = parseFloat((subTotal - totalDiscount)*1.21);

    document.getElementById('subtotal-details').textContent = `$${new Intl.NumberFormat('en-US', { maximumFractionDigits: 2 }).format(subTotal)}`;
    document.getElementById('discount-details').textContent = `-$${new Intl.NumberFormat('en-US', { maximumFractionDigits: 2 }).format(totalDiscount)}`;
    document.getElementById('tax-details').textContent = `$${new Intl.NumberFormat('en-US', { maximumFractionDigits: 2 }).format(tax)}`;
    document.getElementById('total-details').textContent = `$${new Intl.NumberFormat('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 }).format(total)}`;

    const modal = new bootstrap.Modal(document.getElementById('cartDetailsModal'));
    modal.show();
});


document.getElementById('checkout-btn').addEventListener('click', function() {
    const saleDetails = {
        products: cartProducts.map(item => ({
            productId: item.product.id,
            quantity: item.quantity
        })),
        totalPayed: parseFloat(document.getElementById('total-details').textContent.replace(/[$,]/g, ''))
    };

    fetch(saleUrl, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(saleDetails)
    })
    .then(response => {
        if (response.ok) {
           
            const saleModal = new bootstrap.Modal(document.getElementById('confirmationModal'));
            document.getElementById('modal-body').innerHTML = `
            <p>Completed Sale. Thank you for choosing us</p>
        `;
            saleModal.show();
            
        } else {
            const saleModal = new bootstrap.Modal(document.getElementById('confirmationModal'));
            document.getElementById('modal-body').innerHTML = `
            <p>RESPONSE ERROR!!!!</p>
        `;
            saleModal.show();
            
        }
    })
    .catch(error => {
        const saleModal = new bootstrap.Modal(document.getElementById('exampleModal'));
            document.getElementById('modal-body').innerHTML = `
            <p>SERVER CONNECTION ERROR!!!!</p>
        `;
            saleModal.show();
            
    });
});
