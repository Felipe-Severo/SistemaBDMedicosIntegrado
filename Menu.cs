using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBDCompleto
{
    internal class Menu
    {
        //private int _CurrentID = 1; 
        private List<Medico> _Medicos = new List<Medico>();
        public void ChamarMenu()
        {
            Console.WriteLine($"============================");
            Console.WriteLine($"     Cadastros dos Médicos");
            Console.WriteLine($"============================");

            Console.WriteLine($"1 - Listar cadastros (Read/Select) ");
            Console.WriteLine($"2 - Adicionar cadastro (Create/Isert)");
            Console.WriteLine($"3 - Deletar cadastro (Delete)");
            Console.WriteLine($"4 - Atualizar cadastro (Update)");

            switch (Console.ReadLine())
            {
                case "1":
                    var medicos = Medico.GetAll();
                    ListarMedico(medicos);

                    Console.ReadKey();
                    Console.Clear();
                    ChamarMenu();
                    break;

                case "2":
                    Medico medico = new Medico();
                    medico.Save();

                    Console.WriteLine("Cadastro adicionado!!");

                    Console.ReadKey();
                    Console.Clear();
                    ChamarMenu();
                    break;

                case "3":
                    Console.Clear();

                    Console.WriteLine("Informe o ID que deseja deletar: ");
                    var idDeletar = long.Parse(Console.ReadLine());
                    var cadastroDeletar = new Medico(idDeletar);

                    if (cadastroDeletar.IsValid())
                    {
                        cadastroDeletar.Delete();
                        Console.WriteLine("Cadastro excluido!");
                    }
                    else
                    {
                        Console.WriteLine($"Não existe cadastro com o ID {idDeletar}!");
                    }



                    Console.ReadKey();
                    Console.Clear();
                    ChamarMenu();
                    break;

                case "4":
                    Console.WriteLine($"Informe o ID do cadastro: ");
                    var idUpdate = long.Parse(Console.ReadLine());
                    var cadastroUpdate = new Medico(idUpdate);

                    if (cadastroUpdate.IsValid())
                    {
                        AlterarItem(cadastroUpdate);
                    }
                    else
                    {
                        Console.WriteLine($"Não existe cadastro com o ID {idUpdate}");
                    }

                    Console.WriteLine("Produto atualizado!");
                    Console.ReadKey();
                    Console.Clear();
                    ChamarMenu();
                    break;

                default:
                    Console.WriteLine("Opção invalida");

                    Console.ReadKey();
                    Console.Clear();
                    ChamarMenu();
                    break;
            }
        }

        private void AlterarItem(Medico medico)
        {
            Console.Clear();
            Console.WriteLine($"Menu de alteração cadastral");
            Console.WriteLine($"1 - Nome");
            Console.WriteLine($"2 - CRM");
            Console.WriteLine($"3 - Especialização");
            Console.WriteLine($"4 - Senha");
            Console.WriteLine($"'Salvar' para salvar as alterções");

            var entrada = Console.ReadLine().ToUpper();
            while (entrada != "SALVAR")
            {
                if (entrada != "1" && entrada == "2" && entrada != "3" && entrada != "4")
                {
                    Console.WriteLine("Campo invalido");
                }
                else
                {
                    Console.Write($"Informe o valor que será aplicado: ");
                    var valor = Console.ReadLine();


                    switch (entrada)
                    {
                        case "1":
                            medico.Nome = valor;
                            break;

                        case "2":
                            medico.CRM = valor;
                            break;

                        case "3":
                            medico.Especialidade = valor;
                            break;

                        case "4":
                            medico.Senha = valor;
                            break;
                    }
                }

                Console.Clear();
                Console.WriteLine($"Menu de alteração");
                Console.WriteLine($"1 - Nome");
                Console.WriteLine($"2 - CRM");
                Console.WriteLine($"3 - Especialidade");
                Console.WriteLine($"4 - Senha");
                Console.WriteLine($"'Salvar' para salvar as alterções");

                entrada = Console.ReadLine().ToUpper();

            }

            medico.Update();

        }

        //private Produto GerarProduto()
        //{

        //    Produto produto = new Produto();
        //    produto.Save();

        //}


        private void AdicionarItem(Medico medico)
        {
            _Medicos.Add(medico);

        }

        private void ListarMedico(List<Medico> items)
        {
            Console.Clear();
            if (items.Count < 1)
            {
                Console.WriteLine("Sem cadastros para ixibir!");
            }
            foreach (Medico medico in items)
            {
                Console.WriteLine($"Produto ({medico.Id}). \n\tNome: {medico.Nome} \n\tCRM: {medico.CRM} \n\tEspecialidade: {medico.Especialidade} \n\n");
            }
        }

        private void DeletarItem(int id)
        {
            Console.Clear();

            _Medicos.RemoveAll(medico => medico.Id == id);

            //foreach(Produto produto in _Produtos)
            //{
            //    if(produto.Id == id)
            //    {
            //        _Produtos.Remove(produto);
            //    }
            //}
        }
    }
}
