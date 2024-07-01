const saleUrl = "https://localhost:7021/api/Sale/";
const productUrl = "https://localhost:7021/api/Product/";

let currentPage = 1;
const productsPerPage = 6;

document.addEventListener('DOMContentLoaded', () => {
  loadProducts(currentPage);

  document.querySelectorAll('.category-checkbox').forEach(checkbox => {
    checkbox.addEventListener('change', () => {
      if (checkbox.id === 'all-products' && checkbox.checked) {
        document.querySelectorAll('.category-checkbox').forEach(otherCheckbox => {
          if (otherCheckbox !== checkbox) {
            otherCheckbox.checked = false;
          }
        });      
      } else if (checkbox.id !== 'all-products' && checkbox.checked) {
        document.getElementById('all-products').checked = false;
      } else if (checkbox.id === 'all-products' && !checkbox.checked) {
        checkbox.checked = true;
      }
  
      const allUnchecked = document.querySelectorAll('.category-checkbox:checked').length === 0;
      if (allUnchecked) {
        document.getElementById('all-products').checked = true;
      }
  
      currentPage = 1; 
      loadProducts(currentPage);
    });
  });

  document.getElementById('search-form').addEventListener('submit', (e) => {
    e.preventDefault();
    currentPage = 1;
    loadProducts(currentPage);
  });

  document.getElementById('prev-page').addEventListener('click', () => {
    if (currentPage > 1) {
      currentPage--;
      loadProducts(currentPage);
    }
  });

  document.getElementById('next-page').addEventListener('click', () => {
    currentPage++;
    loadProducts(currentPage);
  });
});

async function loadProducts(page) {
  let selectedCategories = [];
  document.querySelectorAll('.category-checkbox:checked').forEach(checkbox => {
    selectedCategories.push(checkbox.value);
  });

  let categoriesQueryString = ''
  if(selectedCategories!=0){
    categoriesQueryString = selectedCategories.map(category => `categories=${category}`).join('&');
  }
  const searchQuery = document.getElementById('search-input').value.trim();
  const offset = (page - 1) * productsPerPage;
  const url = searchQuery
    ? `${productUrl}?&name=${encodeURIComponent(searchQuery)}&limit=${productsPerPage}&offset=${offset}`
    : `${productUrl}?${categoriesQueryString}&limit=${productsPerPage}&offset=${offset}`;

  const loader = document.getElementById('loader');
  const noResults = document.getElementById('no-results');
  const productsContainer = document.getElementById('cards-container');
  const paginationContainer = document.getElementById('pagination-container');

  loader.style.display = 'block';
  noResults.style.display = 'none';
  paginationContainer.style.display = 'block';

  try {
    const response = await fetch(url);
    const products = await response.json();

    loader.style.display = 'none';
    productsContainer.innerHTML = '';

    if (products.length > 0) {
      renderProducts(products);
      document.getElementById('page-info').textContent = `Page ${page}`;
      document.getElementById('prev-page').disabled = (page === 1);
    } else {
      if (page > 1) {
        currentPage--;
        loadProducts(currentPage);
      } else {
        noResults.style.display = 'block';
        paginationContainer.style.display = 'none';       
      }
      
    }
  } catch (error) {
    console.error('Error fetching products:', error);
    loader.style.display = 'none';
  }
}

function renderProducts(products) {
  const productContainer = document.getElementById('cards-container');
  productContainer.innerHTML = '';
  
  products.forEach(product => {
    const price = parseFloat(product.price);
    const discount = parseFloat(product.discount) || 0;
    const discountedPrice = price - ((discount * price) / 100);

    const productCard = `
      <div class="card-container" data-product='${JSON.stringify(product)}'>
        <img src="${product.imageUrl}" alt="${product.name}" class="card-image">
        <div class="card-details">
          <h1 class="card-title">${product.name}</h1>
          <div class="pricing">
            <strong class="price-tittle">Price: </strong>
            <div class="d-flex">
            <p class="original-price">$${new Intl.NumberFormat('en-US', { maximumFractionDigits: 2 }).format(price)}</p>           
            <span class="discount"><svg class="discount-label" xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-tags" viewBox="0 0 16 16">
              <path d="M3 2v4.586l7 7L14.586 9l-7-7zM2 2a1 1 0 0 1 1-1h4.586a1 1 0 0 1 .707.293l7 7a1 1 0 0 1 0 1.414l-4.586 4.586a1 1 0 0 1-1.414 0l-7-7A1 1 0 0 1 2 6.586z"/>
              <path d="M5.5 5a.5.5 0 1 1 0-1 .5.5 0 0 1 0 1m0 1a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3M1 7.086a1 1 0 0 0 .293.707L8.75 15.25l-.043.043a1 1 0 0 1-1.414 0l-7-7A1 1 0 0 1 0 7.586V3a1 1 0 0 1 1-1z"/>
              </svg>${product.discount}% OFF
            </span>
            </div>
            <p class="price">$${new Intl.NumberFormat('en-US', { maximumFractionDigits: 2 }).format(discountedPrice)}</p>
            
            </div>
          <button class="delivery-button">Llega mañana</button>
        </div>
      </div>
    `;
    productContainer.insertAdjacentHTML('beforeend', productCard);
  });

  document.querySelectorAll('.card-container').forEach(card => {
    card.addEventListener('click', (event) => {
      const productData = JSON.parse(card.getAttribute('data-product'));
      showProductDetails(productData);
    });
  });
}


