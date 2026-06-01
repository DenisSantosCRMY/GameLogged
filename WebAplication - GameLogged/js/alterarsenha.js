document.addEventListener('DOMContentLoaded', () => {

  const form = document.getElementById('passwordForm');

  const senha = document.getElementById('novaSenha');
  const confirmar = document.getElementById('confirmarSenha');

  // REGRAS
  const special = document.getElementById('rule-special');
  const upper = document.getElementById('rule-upper');
  const length = document.getElementById('rule-length');

  // VALIDAÇÃO EM TEMPO REAL
  senha.addEventListener('input', () => {

    const value = senha.value;

    validateRule(
      special,
      /[!@#$%]/.test(value)
    );

    validateRule(
      upper,
      /[A-Z]/.test(value)
    );

    validateRule(
      length,
      value.length >= 8
    );

  });

  // SUBMIT
  form.addEventListener('submit', (e) => {

    e.preventDefault();

    const senhaValue = senha.value;
    const confirmarValue = confirmar.value;

    // VALIDAÇÃO
    if(
      senhaValue.length < 8 ||
      !/[A-Z]/.test(senhaValue) ||
      !/[!@#$%]/.test(senhaValue)
    ){
      alert('A senha não atende os requisitos.');
      return;
    }

    if(senhaValue !== confirmarValue){
      alert('As senhas não coincidem.');
      return;
    }

    // SUCESSO
    alert('Senha alterada com sucesso!');

    // REDIRECIONAR
    window.location.href = 'login.html';

  });

});

/* FUNÇÃO */

function validateRule(element, valid){

  if(valid){

    element.classList.add('valid');
    element.innerHTML =
      element.innerHTML.replace('✗', '✓');

  }else{

    element.classList.remove('valid');
    element.innerHTML =
      element.innerHTML.replace('✓', '✗');

  }

}