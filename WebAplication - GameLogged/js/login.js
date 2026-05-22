

const API_BASE = 'http://localhost:5182/api';

document.addEventListener('DOMContentLoaded', () => {

  // Redireciona se já tiver sessão ativa
  if (getSession()) {
    window.location.href = 'perfil.html';
    return;
  }

  // Eye toggle
  const eyeToggle    = document.getElementById('eyeToggle');
  const passwordField = document.getElementById('password');
  if (eyeToggle && passwordField) {
    eyeToggle.addEventListener('click', () => {
      const isText = passwordField.type === 'text';
      passwordField.type = isText ? 'password' : 'text';
    });
  }

  // Submit
  const form      = document.getElementById('loginForm');
  const submitBtn = form?.querySelector('button[type="submit"]');

  form?.addEventListener('submit', async (e) => {
    e.preventDefault();
    clearErrors();

    const email    = document.getElementById('email').value.trim();
    const password = document.getElementById('password').value;
    let valid = true;

    if (!email || !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email)) {
      showFieldError('email', 'Informe um e-mail válido.'); valid = false;
    }
    if (!password || password.length < 6) {
      showFieldError('password', 'Mínimo 6 caracteres.'); valid = false;
    }
    if (!valid) return;

    setLoading(true);

    try {
      const res  = await fetch(`${API_BASE}/auth/login`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email, password })
      });
      const data = await res.json();

      if (!res.ok) {
        showGlobalError(data.message || 'E-mail ou senha incorretos.');
        setLoading(false);
        return;
      }

      // Salva sessão e redireciona
      saveSession({ id: data.id, nickname: data.nickname, nome: data.nome, email: data.email });
      window.location.href = 'perfil.html';

    } catch {
      showGlobalError('Não foi possível conectar ao servidor. Verifique se a API está rodando.');
      setLoading(false);
    }
  });

  // ── Helpers ──────────────────────────────
  function saveSession(user) { sessionStorage.setItem('gl_user', JSON.stringify(user)); }
  function getSession()      { try { return JSON.parse(sessionStorage.getItem('gl_user')); } catch { return null; } }

  function setLoading(on) {
    if (!submitBtn) return;
    submitBtn.disabled    = on;
    submitBtn.textContent = on ? 'Entrando…' : 'Entrar';
  }

  function showGlobalError(msg) {
    clearGlobalError();
    const el = document.createElement('div');
    el.id = 'gl-error';
    el.style.cssText = 'background:rgba(255,82,82,.12);border:1px solid rgba(255,82,82,.35);border-radius:10px;color:#ff5252;font-size:.85rem;padding:10px 14px;margin-bottom:14px;text-align:center;';
    el.textContent = msg;
    form.insertBefore(el, form.firstChild);
  }
  function clearGlobalError() { document.getElementById('gl-error')?.remove(); }

  function showFieldError(id, msg) {
    const field = document.getElementById(id);
    if (!field) return;
    field.style.borderColor = 'var(--accent-red)';
    const span = document.createElement('span');
    span.className = 'field-error';
    span.textContent = msg;
    field.closest('.input-group')?.appendChild(span);
  }
  function clearErrors() {
    clearGlobalError();
    document.querySelectorAll('.field-error').forEach(el => el.remove());
    document.querySelectorAll('.input-field').forEach(el => el.style.borderColor = '');
  }
});
