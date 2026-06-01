// =============================================
// GameLogged — Perfil (UI Only, sem API)
// =============================================

document.addEventListener('DOMContentLoaded', () => {

  // ── Tab switching
  const tabBtns    = document.querySelectorAll('.tab-btn');
  const tabContents = document.querySelectorAll('.tab-content');

  tabBtns.forEach(btn => {
    btn.addEventListener('click', () => {
      const target = btn.dataset.tab;

      tabBtns.forEach(b => b.classList.remove('active'));
      btn.classList.add('active');

      tabContents.forEach(tc => {
        const match = tc.id === `tab-${target}`;
        tc.classList.toggle('hidden', !match);
      });
    });
  });

  // ── Follow toggle (mock)
  const followBtn = document.querySelector('.btn-follow');
  if (followBtn) {
    let following = false;
    followBtn.addEventListener('click', () => {
      following = !following;
      followBtn.textContent = following ? '✓ Seguindo' : '+ Seguir';
      followBtn.style.background = following
        ? 'rgba(108,99,255,0.2)'
        : '';
      if (!following) {
        followBtn.style.background = 'var(--gradient-brand)';
      }
    });
  }

});
