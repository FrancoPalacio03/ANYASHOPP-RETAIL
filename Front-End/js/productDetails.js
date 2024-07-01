function showProductDetails(product) {
  document.getElementById('product-quantity').value = 1;
  fetch(`${productUrl}${product.id}`)
    .then(response => response.json())
    .then(data => {
      const formattedPrice = new Intl.NumberFormat('en-US', { maximumFractionDigits: 2 }).format(data.price);
      const discountedPrice = new Intl.NumberFormat('en-US', { maximumFractionDigits: 2 }).format(data.price - ((data.discount * data.price) / 100));

      document.getElementById('product-title').textContent = data.name;
      document.getElementById('product-price').textContent = `$${formattedPrice}`;
      document.getElementById('product-price-especial').textContent = `$${discountedPrice}`;
      document.getElementById('product-image').src = data.imageUrl;
      document.getElementById('product-description').textContent = data.description;
      const modal = new bootstrap.Modal(document.getElementById('productModal'));
      modal.show();

      const addToCartButton = document.getElementById('cart-button');
      addToCartButton.onclick = () => {
        const quantity = document.getElementById('product-quantity').value;
        addToCart(product, quantity);
      };
    })
    .catch(error => console.error('Error fetching product details:', error));
}


document.addEventListener('DOMContentLoaded', () => {
  const quantityInput = document.getElementById('product-quantity');
  const decreaseButton = document.querySelector('.btn-decrease');
  const increaseButton = document.querySelector('.btn-increase');

  decreaseButton.addEventListener('click', () => {
    let currentValue = parseInt(quantityInput.value);
    if (currentValue > 1) {
      quantityInput.value = currentValue - 1;
    }
  });

  increaseButton.addEventListener('click', () => {
    let currentValue = parseInt(quantityInput.value);
    quantityInput.value = currentValue + 1;
  });
});

const toastTrigger = document.getElementById('cart-button')
const toastLiveExample = document.getElementById('liveToast')

if (toastTrigger) {
  const toastBootstrap = bootstrap.Toast.getOrCreateInstance(toastLiveExample)
  toastTrigger.addEventListener('click', () => {
    toastBootstrap.show()
  })
}