window.addEventListener("load", function () {
  const customLockedSVG = `
    <svg class="custom-lock" xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#1976d2" viewBox="0 0 24 24">
      <path d="M12 17a1.5 1.5 0 0 0 0-3 1.5 1.5 0 0 0 0 3zm6-7h-1V7a5 5 0 0 0-10 0v3H6a2 2 0 0 0-2 2v9a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2v-9a2 2 0 0 0-2-2zm-8-3a3 3 0 0 1 6 0v3h-6V7zm8 14H6v-9h12v9z"/>
    </svg>
  `;

  const customUnlockedSVG = `
    <svg class="custom-lock" xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#d32f2f" viewBox="0 0 24 24">
      <path d="M12 17a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3zm6-7h-8V7a3 3 0 1 1 6 0h2a5 5 0 0 0-10 0v3H6a2 2 0 0 0-2 2v9a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2v-9a2 2 0 0 0-2-2zm0 11H6v-9h12v9z"/>
    </svg>
  `;

  function updateButtonIcon() {
    const btn = document.querySelector('.authorization__btn');
    if (!btn) return;

    // Remove ícone original
    const defaultSVG = btn.querySelector('svg');
    if (defaultSVG) defaultSVG.style.display = 'none';

    // Remove ícone custom anterior
    const oldCustom = btn.querySelector('.custom-lock');
    if (oldCustom) oldCustom.remove();

    // Verifica se está autenticado
    const isLocked = btn.getAttribute('aria-label')?.includes('locked');

    // Cria wrapper e insere SVG novo
    const wrapper = document.createElement('span');
    wrapper.innerHTML = isLocked ? customLockedSVG : customUnlockedSVG;

    btn.insertBefore(wrapper.firstChild, btn.firstChild);
  }

  // Tenta várias vezes até o Swagger renderizar o botão
  let tries = 0;
  const maxTries = 20;

  const interval = setInterval(() => {
    tries++;
    updateButtonIcon();

    if (document.querySelector('.authorization__btn')) {
      clearInterval(interval); // Para quando o botão for encontrado
    }

    if (tries >= maxTries) {
      clearInterval(interval); // Evita loop infinito
    }
  }, 500);
});
