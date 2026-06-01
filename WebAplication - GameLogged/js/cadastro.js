// =============================================
// GameLogged — Cadastro (UI Only, sem API)
// =============================================

document.addEventListener('DOMContentLoaded', () => {

  // ── Eye toggles
  setupEyeToggle('eyeSenha', 'senha');
  setupEyeToggle('eyeConfirm', 'confirmarSenha');

  function setupEyeToggle(btnId, fieldId) {
    const btn   = document.getElementById(btnId);
    const field = document.getElementById(fieldId);
    if (!btn || !field) return;
    btn.addEventListener('click', () => {
      const isText = field.type === 'text';
      field.type = isText ? 'password' : 'text';
    });
  }

  // ── Date mask: DD / MM / AAAA
  const dataNasc = document.getElementById('dataNasc');
  if (dataNasc) {
    dataNasc.addEventListener('input', () => {
      let v = dataNasc.value.replace(/\D/g, '').slice(0, 8);
      if (v.length >= 3) v = v.slice(0,2) + ' / ' + v.slice(2);
      if (v.length >= 9) v = v.slice(0,7) + ' / ' + v.slice(7);
      dataNasc.value = v;
    });
  }

  // ── Password hints live
  const senhaField = document.getElementById('senha');
  if (senhaField) {
    senhaField.addEventListener('input', () => {
      const v = senhaField.value;
      setHint('hint-special', /[!@#$%]/.test(v));
      setHint('hint-upper',   /[A-Z]/.test(v));
      setHint('hint-length',  v.length >= 8);
    });
  }

  function setHint(id, ok) {
    const el = document.getElementById(id);
    if (!el) return;
    const msgs = {
      'hint-special': 'Pelo menos 1 caractere especial (! @ # $ %)',
      'hint-upper':   'Pelo menos 1 letra maiúscula (A-Z)',
      'hint-length':  'No mínimo 8 caracteres',
    };
    el.textContent = (ok ? '✓ ' : '✗ ') + msgs[id];
    el.classList.toggle('ok', ok);
  }

  // ── Form submit (UI validation only – sem API)
  const form = document.getElementById('registerForm');
  if (form) {
    form.addEventListener('submit', (e) => {
      e.preventDefault();
      clearErrors();
      let valid = true;

      const nome    = document.getElementById('nome').value.trim();
      const nick    = document.getElementById('nickname').value.trim();
      const data    = document.getElementById('dataNasc').value.trim();
      const email   = document.getElementById('email').value.trim();
      const senha   = document.getElementById('senha').value;
      const confirm = document.getElementById('confirmarSenha').value;
      const terms   = document.getElementById('terms').checked;

      if (!nome)   { showError('nome', 'Informe seu nome.'); valid = false; }
      if (!nick)   { showError('nickname', 'Informe um NickName.'); valid = false; }
      if (!data || data.replace(/\D/g,'').length < 8) {
        showError('dataNasc', 'Informe sua data de nascimento.'); valid = false;
      }
      if (!email || !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email)) {
        showError('email', 'Informe um e-mail válido.'); valid = false;
      }
      if (!isPasswordValid(senha)) {
        showError('senha', 'Senha não atende aos requisitos.'); valid = false;
      }
      if (senha !== confirm) {
        showError('confirmarSenha', 'As senhas não coincidem.'); valid = false;
      }
      if (!terms) {
        alert('Você precisa aceitar os Termos de Serviço para continuar.');
        valid = false;
      }

      if (valid) {
        // Placeholder: redirect to perfil
        window.location.href = 'perfil.html';
      }
    });
  }

  // ── Helpers
  function isPasswordValid(p) {
    return p.length >= 8 && /[A-Z]/.test(p) && /[!@#$%]/.test(p);
  }

  function showError(fieldId, msg) {
    const field = document.getElementById(fieldId);
    if (!field) return;
    field.style.borderColor = 'var(--accent-red)';
    const err = document.createElement('span');
    err.className = 'field-error';
    err.textContent = msg;
    field.closest('.input-group').appendChild(err);
  }

  function clearErrors() {
    document.querySelectorAll('.field-error').forEach(el => el.remove());
    document.querySelectorAll('.input-field').forEach(el => {
      el.style.borderColor = '';
    });
  }

});
