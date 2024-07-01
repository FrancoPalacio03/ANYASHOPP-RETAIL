let cartProducts = [];
const btnCart = document.querySelector('.container-cart-icon');
const containerCartProducts = document.querySelector('.container-cart-products');

btnCart.addEventListener('click', () => {
    containerCartProducts.classList.toggle('hidden-cart');
});

const saleBtn = document.querySelector('.sale-btn-container');
const valorTotal = document.querySelector('.total-pagar');
const countProducts = document.querySelector('#contador-productos');
const cartEmpty = document.querySelector('.cart-empty');
const cartTotal = document.querySelector('.cart-total');
const rowProduct = document.querySelector('.row-product');


function addToCart(product, quantity) {
    const infoProduct = {
        product : product, 
        quantity: parseInt(quantity), 
    };



    const exists = cartProducts.some(item => item.product.id === infoProduct.product.id);
    if (exists) {      
        cartProducts.forEach(item => {
            if(item.product.id === infoProduct.product.id){
            item.quantity += infoProduct.quantity;
        }            
        });
    } else {
        cartProducts.push(infoProduct);
    }
    updateCart();
}

function removeFromCart(title) {
    cartProducts = cartProducts.filter(item => item.product.name !== title); 
    updateCart();
}

function updateCart() {
    if (cartProducts.length === 0) {
        cartEmpty.classList.remove('hidden');
        rowProduct.innerHTML = ''; 
        cartTotal.classList.add('hidden');
        saleBtn.classList.add('hidden');
    } else {
        cartEmpty.classList.add('hidden');
        cartTotal.classList.remove('hidden');
        saleBtn.classList.remove('hidden');

        let total = 0;
        let totalOfProducts = 0;

        rowProduct.innerHTML = '';
        cartProducts.forEach(productToShow => {
            const containerProduct = document.createElement('div');
            containerProduct.classList.add('cart-product');
            containerProduct.innerHTML = `
                <div class="info-cart-product">
                <span class="quantity-label">x</span>
                    <input type="number" step="1" min="1" name="quantity" value="${productToShow.quantity}" class="quantity-field border-0 text-center cart-product-quantity" data-id="${productToShow.product.id}">
                    <p class="titulo-producto-carrito">${productToShow.product.name}</p>
                    <span class="precio-producto-carrito">$${new Intl.NumberFormat('en-US', { maximumFractionDigits: 2 }).format(productToShow.product.price)}</span>
                </div>
                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor" class="icon-close cursor-pointer" data-title="${productToShow.product.name}">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12" />
                </svg>
            `;
            rowProduct.appendChild(containerProduct);

            total += parseInt(productToShow.quantity) * parseFloat(productToShow.product.price);
        });

        valorTotal.innerText = `$${new Intl.NumberFormat('en-US', {maximumFractionDigits: 2 }).format(total)}`;
    }
    countProducts.innerText = cartProducts.length;
}


rowProduct.addEventListener('change', e => {
    if (e.target.classList.contains('cart-product-quantity')) {
        const productId = e.target.getAttribute('data-id');
        const newQuantity = parseInt(e.target.value);
        updateQuantity(productId, newQuantity);
    }
});

rowProduct.addEventListener('click', e => {
    if (e.target.classList.contains('icon-close')) {
        const product = e.target.parentElement;
        const title = product.querySelector('p').textContent;
        removeFromCart(title);
    }
});

function updateQuantity(productId, newQuantity) {
    cartProducts.forEach(item => {
        if (item.product.id === productId) {
            item.quantity = newQuantity;
            if (item.quantity < 1) item.quantity = 1; // Evitar que la cantidad sea menor que 1
        }
    });
    updateCart();
}


