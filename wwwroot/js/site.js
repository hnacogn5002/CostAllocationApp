// ===== COST ALLOCATION APP - SITE.JS =====

// Format currency VND
function formatVND(amount) {
    return new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format(amount);
}
function formatVNDNum(amount) {
    return new Intl.NumberFormat('vi-VN').format(amount) + ' ₫';
}

// ===== LIVE ALLOCATION CALCULATOR =====
function setupAllocationCalculator() {
    const rateInput = document.getElementById('AllocationRate');
    const costSelect = document.getElementById('OverheadCostId');
    const previewEl = document.getElementById('allocatedAmountPreview');
    if (!rateInput || !costSelect || !previewEl) return;

    function updatePreview() {
        const costAmounts = window.costAmounts || {};
        const selectedId = costSelect.value;
        const totalAmount = parseFloat(costAmounts[selectedId] || 0);
        const rate = parseFloat(rateInput.value || 0);
        if (totalAmount > 0 && rate >= 0 && rate <= 100) {
            const allocated = totalAmount * rate / 100;
            previewEl.textContent = formatVNDNum(allocated);
            previewEl.closest('.preview-box')?.classList.remove('d-none');
        } else {
            previewEl.textContent = '—';
        }
    }
    rateInput.addEventListener('input', updatePreview);
    costSelect.addEventListener('change', updatePreview);
    updatePreview();
}

// ===== FORM VALIDATION =====
function setupFormValidation() {
    const forms = document.querySelectorAll('.needs-validation');
    forms.forEach(form => {
        form.addEventListener('submit', event => {
            let valid = true;

            // Custom: AllocationRate range 0-100
            const rateField = form.querySelector('#AllocationRate');
            if (rateField) {
                const v = parseFloat(rateField.value);
                if (isNaN(v) || v < 0 || v > 100) {
                    rateField.setCustomValidity('Tỷ lệ phân bổ phải từ 0 đến 100');
                    valid = false;
                } else {
                    rateField.setCustomValidity('');
                }
            }

            // Custom: TotalAmount > 0
            const amountField = form.querySelector('#TotalAmount');
            if (amountField) {
                const v = parseFloat(amountField.value);
                if (isNaN(v) || v <= 0) {
                    amountField.setCustomValidity('Số tiền phải lớn hơn 0');
                    valid = false;
                } else {
                    amountField.setCustomValidity('');
                }
            }

            if (!form.checkValidity() || !valid) {
                event.preventDefault();
                event.stopPropagation();
            }
            form.classList.add('was-validated');
        });
    });
}

// ===== DELETE CONFIRM =====
function confirmDelete(name, formId) {
    document.getElementById('deleteItemName').textContent = name;
    document.getElementById('deleteFormTarget').value = formId;
    const modal = new bootstrap.Modal(document.getElementById('deleteModal'));
    modal.show();
}
function setupDeleteConfirm() {
    const btn = document.getElementById('confirmDeleteBtn');
    if (!btn) return;
    btn.addEventListener('click', () => {
        const formId = document.getElementById('deleteFormTarget').value;
        const form = document.getElementById(formId);
        if (form) form.submit();
    });
}

// ===== SEARCH & FILTER TABLE =====
function setupTableSearch() {
    const searchInput = document.getElementById('tableSearch');
    if (!searchInput) return;
    searchInput.addEventListener('input', function () {
        const query = this.value.toLowerCase();
        document.querySelectorAll('.searchable-row').forEach(row => {
            row.style.display = row.textContent.toLowerCase().includes(query) ? '' : 'none';
        });
    });
}

// ===== TOAST NOTIFICATION =====
function showToast(message, type = 'success') {
    const container = document.getElementById('toastContainer');
    if (!container) return;
    const id = 'toast-' + Date.now();
    const icon = type === 'success' ? '✅' : '❌';
    const bg   = type === 'success' ? '#e8f5e9' : '#ffebee';
    const col  = type === 'success' ? '#2e7d32' : '#c62828';
    container.insertAdjacentHTML('beforeend', `
      <div id="${id}" class="toast align-items-center border-0" role="alert" style="background:${bg};color:${col};border-radius:10px;">
        <div class="d-flex">
          <div class="toast-body fw-semibold">${icon} ${message}</div>
          <button type="button" class="btn-close me-2 m-auto" data-bs-dismiss="toast"></button>
        </div>
      </div>`);
    const toastEl = document.getElementById(id);
    new bootstrap.Toast(toastEl, { delay: 3500 }).show();
}

// ===== COUNTER ANIMATION =====
function animateCounters() {
    document.querySelectorAll('[data-counter]').forEach(el => {
        const target = parseInt(el.dataset.counter, 10);
        let current = 0;
        const step = Math.ceil(target / 30);
        const timer = setInterval(() => {
            current = Math.min(current + step, target);
            el.textContent = current.toLocaleString('vi-VN');
            if (current >= target) clearInterval(timer);
        }, 40);
    });
}

// ===== INIT =====
document.addEventListener('DOMContentLoaded', () => {
    setupFormValidation();
    setupAllocationCalculator();
    setupDeleteConfirm();
    setupTableSearch();
    animateCounters();

    // Active nav link
    const current = window.location.pathname.split('/')[1] || 'Home';
    document.querySelectorAll('.nav-link').forEach(link => {
        const href = link.getAttribute('href') || '';
        if (href.includes(current) && current !== 'Home') {
            link.classList.add('active');
        }
    });
});
