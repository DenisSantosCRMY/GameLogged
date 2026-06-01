document.addEventListener('DOMContentLoaded', () => {

  const form = document.getElementById('recoverForm');
  const emailField = document.getElementById('email');

  form.addEventListener('submit', async (e) => {

    e.preventDefault();

    emailField.classList.remove('input-error');

    const email = emailField.value.trim();

    // VALIDAR EMAIL
    if(email === ''){
      alert('Digite o seu e-mail.');
      emailField.classList.add('input-error');
      return;
    }

    const emailValid =
      /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email);

    if(!emailValid){
      alert('Digite um e-mail válido.');
      emailField.classList.add('input-error');
      return;
    }

    // BOTÃO LOADING
    const button = document.querySelector('.submit-button');

    button.disabled = true;
    button.textContent = 'Enviando...';

    try {

      // FUTURA API
      const response = await fetch(
        'http://localhost:8080/auth/forgot-password',
        {
          method: 'POST',

          headers: {
            'Content-Type': 'application/json'
          },

          body: JSON.stringify({
            email: email
          })
        }
      );

      // CONVERTE RESPOSTA
      const data = await response.json();

      // SUCESSO
      if(response.ok){

        alert(data.message || 'Link enviado com sucesso.');

      } else {

        alert(data.message || 'Erro ao enviar link.');

      }

    } catch(error){

      console.error(error);

      alert('Erro de conexão com o servidor.');

    } finally {

      button.disabled = false;
      button.textContent = 'Enviar Link';

    }

  });

});