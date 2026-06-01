// =============================================
// GameLogged — Login (UI Only, sem API)
// =============================================

document.addEventListener('DOMContentLoaded', () => {

  // ── Eye toggle
  const eyeToggle = document.getElementById('eyeToggle');
  const passwordField = document.getElementById('password');

  if (eyeToggle && passwordField) {
    eyeToggle.addEventListener('click', () => {
      const isText = passwordField.type === 'text';
      passwordField.type = isText ? 'password' : 'text';
      eyeToggle.setAttribute('aria-label', isText ? 'Ver senha' : 'Ocultar senha');
    });
  }

  // ── Form submit (UI feedback only – sem API)
  const form = document.getElementById('loginForm');
  if (form) {
    form.addEventListener('submit', (e) => {
      e.preventDefault();
      const email    = document.getElementById('email').value.trim();
      const password = document.getElementById('password').value;
      let valid = true;

      clearErrors();

      if (!email || !isValidEmail(email)) {
        showError('email', 'Informe um e-mail válido.');
        valid = false;
      }
      if (!password || password.length < 6) {
        showError('password', 'Senha deve ter no mínimo 6 caracteres.');
        valid = false;
      }

      if (valid) {
        // Placeholder: redireciona para perfil (mock)
        window.location.href = 'perfil.html';
      }
    });
  }

  // ── Helpers
  function isValidEmail(v) {
    return /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(v);
  }

  function showError(fieldId, msg) {
    const field = document.getElementById(fieldId);
    if (!field) return;
    field.closest('.input-group').classList.add('has-error');
    const err = document.createElement('span');
    err.className = 'field-error';
    err.textContent = msg;
    field.closest('.input-group').appendChild(err);
    field.style.borderColor = 'var(--accent-red)';
  }

  function clearErrors() {
    document.querySelectorAll('.field-error').forEach(el => el.remove());
    document.querySelectorAll('.input-field').forEach(el => {
      el.style.borderColor = '';
    });
  }

});
