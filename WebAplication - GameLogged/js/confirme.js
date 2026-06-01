document.addEventListener('DOMContentLoaded', () => {

  const btnRetornar = document.getElementById('btnRetornar');
  const btnEnviar = document.getElementById('btnEnviar');

  // BOTÃO RETORNAR
  btnRetornar.addEventListener('click', () => {

    // ALTERE PARA SUA TELA
    window.location.href = 'login.html';

  });

  // BOTÃO ENVIAR LINK
  btnEnviar.addEventListener('click', () => {

    btnEnviar.disabled = true;
    btnEnviar.textContent = 'Enviando...';

    // SIMULAÇÃO
    setTimeout(() => {

      alert('Novo link enviado com sucesso!');

      btnEnviar.disabled = false;
      btnEnviar.textContent = 'Enviar Link';

    }, 2000);

  });

});