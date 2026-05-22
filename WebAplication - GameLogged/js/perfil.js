

document.addEventListener('DOMContentLoaded', () => {

  // ── Sessão ───────────────────────────────
  function getSession() { try { return JSON.parse(sessionStorage.getItem('gl_user')); } catch { return null; } }
  function logout()     { sessionStorage.removeItem('gl_user'); window.location.href = 'login.html'; }

  const user = getSession();
  if (!user) { window.location.href = 'login.html'; return; }

  // ── Preenche dados ────────────────────────
  const nameEl     = document.getElementById('profileName');
  const handleEl   = document.getElementById('profileHandle');
  const emailEl    = document.getElementById('profileEmail');
  const postNameEl = document.getElementById('postAuthorName');

  if (nameEl)     nameEl.textContent     = user.nome || user.nickname;
  if (handleEl)   handleEl.textContent   = '@' + user.nickname;
  if (emailEl)    emailEl.textContent    = user.email;
  if (postNameEl) postNameEl.textContent = user.nickname;

  document.title = `${user.nickname} — GameLogged`;

  // ── Menu / Logout ─────────────────────────
  const menuBtn      = document.getElementById('menuBtn');
  const menuDropdown = document.getElementById('menuDropdown');

  menuBtn?.addEventListener('click', (e) => {
    e.stopPropagation();
    menuDropdown?.classList.toggle('open');
  });
  document.addEventListener('click', () => menuDropdown?.classList.remove('open'));
  menuDropdown?.addEventListener('click', e => e.stopPropagation());
  document.getElementById('logoutBtn')?.addEventListener('click', logout);

  // ── Botão Seguir ──────────────────────────
  const followBtn = document.getElementById('followBtn');
  followBtn?.addEventListener('click', () => {
    const following = followBtn.classList.toggle('is-following');
    followBtn.querySelector('.follow-label').textContent = following ? 'Seguindo' : 'Seguir';
  });

  // ── Tabs ──────────────────────────────────
  document.querySelectorAll('.tab-btn').forEach(btn => {
    btn.addEventListener('click', () => {
      document.querySelectorAll('.tab-btn').forEach(b => b.classList.remove('active'));
      document.querySelectorAll('.tab-pane').forEach(p => p.classList.add('hidden'));
      btn.classList.add('active');
      document.getElementById('tab-' + btn.dataset.tab)?.classList.remove('hidden');
    });
  });
});
