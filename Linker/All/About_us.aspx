<%@ Page Title="" Language="C#" MasterPageFile="~/Linker.Master" AutoEventWireup="true"
    CodeBehind="About_us.aspx.cs" Inherits="Linker.All.About_us" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="alignjustify">
        <b>O tema escolhido?</b>
        <hr />
        <p style="text-indent: 36px">
            O projecto escolhido tem como finalidade a partilha de conteúdos a partir da disponibilização
            de links.
        </p>
        <br />
        <b>Motivações para o tema?</b>
        <hr />
        <p style="text-indent: 36px">
            Decidi escolher este tema pois não conheço nenhum site que tenha este tipo de funcionalidade.
            O projecto em si não é complexo, mas penso que seja minimamente útil. Acho que também
            vale a pena referir, que apesar de ser um site de partilha de imagens e vídeos este
            não requer um servidor de grande capacidade para se manter, devido à própria natureza
            do projecto.
        </p>
        <br />
        <b>Quem fez o projecto?</b>
        <hr />
        <p style="text-indent: 36px">
            Este projecto foi desenvolvido por Filipe Carvalho.
        </p>
        <br />
        <b>O âmbito em que o projecto foi desenvolvido?</b>
        <hr />
        <p style="text-indent: 36px">
            Este projecto foi desenvolvido no âmbito da avaliação final da disciplina de Programação
            Web.
        </p>
        <br />
        <hr />
        <hr />
        <br />
        <b>O que é a .Net Framework?</b>
        <hr />
        <p style="text-indent: 36px">
            A .NET Framework, mais concretamente Microsoft .NET Framework, entende-se como sendo
            uma plataforma única para desenvolvimento e execução de sistemas e aplicações. Todo
            e qualquer código gerado para esta plataforma .NET pode ser executado em qualquer
            dispositivo que possua uma framework de tal plataforma. Esta é uma ideia semelhante
            à plataforma Java, onde o programador deixa de escrever código para um sistema ou
            dispositivo específico, e passa a escrever para a plataforma .NET.
        </p>
        <br />
        <b>Como é constituída?</b>
        <hr />
        <p style="text-indent: 36px">
            A .Net Framework é essencial para correr vários programas que sejam desenvolvidos
            com o uso dessa tecnologia da Microsoft. Além de segurança, a tecnologia oferece
            independência de plataforma, com algumas aplicações a correr em Linux, por exemplo.
            Para estes softwares correm correctamente, ele necessitam de diversos componentes
            que foram usados pelo programador e devem estar instalados na hora da execução.
            O download da versão 2.0 é grande, cerca de 23 MB, e cada vez mais programas têm
            sido desenvolvidos usando essa plataforma.
        </p>
        <br />
        <b>Como é a sua arquitectura?</b>
        <hr />
        <div style="text-indent: 36px">
            A plataforma .NET baseia-se num dos princípios utilizados na tecnologia Java (Just
            In Time Compiler - JIT), os programas desenvolvidos para ela são duplo-compilados
            (compilados duas vezes), uma na distribuição (gerando um código que é conhecido
            como "bytecodes") e outra na execução. Um programa é escrito em qualquer das mais
            de vinte linguagens de programação disponíveis para a plataforma, o código fonte
            gerado pelo programador é então compilado pela linguagem escolhida gerando um código
            intermediário numa linguagem chamada MSIL (Microsoft Intermediate Language). Este
            novo código fonte gera um arquivo na linguagem de baixo nível Assembly, de acordo
            com o tipo de projeto:
            <br />
            <p style="text-indent: 0px; margin-left: 56px">
                ● EXE - Arquivos Executáveis, Programas;
                <br />
                ● DLL - Biblioteca de Funções;
                <br />
                ● ASPX - Página Web;
                <br />
                ● ASMX - Web Service.
            </p>
            <p>
                No momento da execução do programa ele é novamente compilado, desta vez pelo compilador
                JIT, de acordo com a utilização do programa.
            </p>
            <center>
                <br />
                <img id="Image1" alt="ADO .NET Casses" src="../images/image01.png" />
            </center>
        </div>
        <br />
        <b>Que linguagem podemos utilizar para desenvolver para esta Framework?</b>
        <hr />
        <p style="text-indent: 0px; margin-left: 56px">
            ● Visual Basic;
            <br />
            ● C#;
            <br />
            ● C++;
        </p>
        <br />
        <b>Que ferramentas podemos utilizar para o fazer?</b>
        <hr />
        <p style="text-indent: 0px; margin-left: 56px">
            ● Notepad++;
            <br />
            ● Visual Studio;
            <br />
            ● WebMatrix;
        </p>
        <br />
        <b>Qual é a diferença entre código do lado do cliente (ClientSide) e código do lado
            do servidor (ServerSide)?</b>
        <hr />
        <div style="text-indent: 36px">
            <b>Client-side</b> traduz-se numa linguagem de cliente, que na prática se resume
            a uma linguagem utilizada no computador do próprio cliente. Como tal, esta é usada
            nas situações em que a linguagem server-side não tem alcance. Tambem é de relevo
            mencionar que esta llinguagem não é propriamente confiável pois está a ser executada
            do lado do cliente e como tal está sujeita a alterações em run-time. Entre as linguagens
            client-side, há o JavaScript, que é a única linguagem que realmente corre no navegador
            do utilizador.
            <p>
                <b>Server-side</b> traduz-se numa linguagem de servidor, que na prática se resume
                ao contrário da linguagem de cliente, uma vez que esta não é utilizada no computador
                do próprio cliente mas sim no servidor. Deste modo, sempre que o utilizador faz
                um pedido HTTP, este é enviado para o servidor de destino e a linguagem de servidor
                recebe o pedido e faz o seu processamento. Como por exemplo, cabe à linguagem de
                servidor fazer a verificação do utilizador logado, procurar informações na base
                de dados, etc.
            </p>
            <p>
                Como a linguagem server-side processa as coisas antes de enviar para o navegador,
                isso significa que, uma vez que a página seja enviada ao navegador do utilizador,
                não há mais nada que a linguagem server-side possa fazer até que um novo pedido
                seja enviado. Ou seja, não é possível usar estas linguagens para manipular a página
                do utilizador em tempo real. Para o utilizador, a linguagem server-side não importa,
                e ele nem tem como descobrir qual a linguagem que está a ser usada.
            </p>
        </div>
        <br />
        <b>Alguns dos principais componentes para elaborar um formulário?</b>
        <hr />
        <div style="text-indent: 36px">
            Como principais componentes temos:
            <br />
            <p style="text-indent: 0px; margin-left: 56px">
                ● Textboxs;
                <br />
                ● Botões;
                <br />
                ● Text Area;
                <br />
                ● Labels;
                <br />
                ● Legend;
                <br />
                ● FieldSet.
            </p>
        </div>
        <br />
        <b>O que é um Postback?</b>
        <hr />
        <p style="text-indent: 36px">
            De um modo simples, o postback entende-se como sendo uma medida tomada por uma página
            interactiva. De um modo explicativo, a página e o seu conteúdo são enviados ao servidor
            para processamento de algumas informações e em seguida, o servidor dá retorno da
            mesma página ao navegador do cliente.
        </p>
        <br />
        <b>O que são o Request e o Response?</b>
        <hr />
        <p style="text-indent: 36px">
            Response e Request são objectos que ainda existem e podem ser usados em páginas
            ASP.NET. Estes objectos representam a informação enviada para o servidor Web a partir
            do navegador (request) e a informação vinda do servidor para o navegador (response).
            O objecto request representa o objecto input e o objecto response representa o objecto
            output.
        </p>
        <br />
        <b>O que é o Web.Config?</b>
        <hr />
        <p style="text-indent: 36px">
            Web.config é o ficheiro principal de configurações numa aplicação web de ASP.NET.
            O ficheiro é um documento XML que define as informações de configuração em relação
            à aplicação web. Este, pode conter informações sobre as configurações de carregamento
            de módulos, configurações de segurança, configurações de estado de sessão, e ainda
            itens de aplicação específicos, tais como sequências para a conexão à base de dados.
        </p>
        <br />
        <b>O que é o Global Application Class?</b>
        <hr />
        <div style="text-indent: 36px">
            Global Application Class (.asax), é um ficheiro opcional, apenas podendo existir
            um mas de muita importância. Permite declarar e lidar com a aplicação ao nível de
            sessão de eventos e objectos para um site ASP.NET num servidor Web IIS. O ficheiro
            Global.asax encontra-se na raiz virtual do IIS de uma aplicação ASP.NET.
            <br />
            <p>
                Os métodos mais utilizados são:
            </p>
            <p style="text-indent: 0px; margin-left: 56px">
                ● <b>Application_Start</b>: Acontece quando a aplicação inicia;
                <br />
                ● <b>Session_Start</b>: Acontece quando a sessão do utilizador inicia;
                <br />
                ● <b>Application_BeginRequest</b>: Acontece cada vez que é feito um request;
                <br />
                ● <b>Session_End</b>: Acontece quando a sessão termina ou faz time out;
                <br />
                ● <b>Application_End</b>: Acontece quando a aplicação termina ou faz time out.
            </p>
        </div>
        <br />
        <b>O que é um Generic Handler?</b>
        <hr />
        <p style="text-indent: 36px">
            Generic Handler (.ashx) é um Handler HTTP genérico que funciona como uma página
            ASPX normal mas sem a parte HTML. Os valores têm de ser passados por QueryString.
            Uma boa utilização para este objecto é a apresentação de imagens.
        </p>
        <br />
        <b>O que é a QueryString?</b>
        <hr />
        <div style="text-indent: 36px">
            A QueryString é o pedaço de texto a seguir ao “?” que permite enviar informação
            entre páginas.
            <p style="text-indent: 36px">
                Como exemplo, através de uma pesquisa feita no motor de busca “google” com a palavra
                “pplware”, resulta no seguinte link <b>http://www.google.pt/search?q=pplware</b>;
                neste caso define uma única variável “q”, que contém o valor “pplware”.
            </p>
            <p style="text-indent: 36px">
                A vantagem da QueryString é ser leve e não trazer nenhum fardo para o servidor.
                A desvantagem é os valores estarem visíveis para o utilizador e podem ser alterados.
            </p>
        </div>
        <br />
        <b>O que é o Ciclo de Vida de uma página?</b>
        <hr />
        <div style="text-indent: 36px">
            Quando uma página ASP.NET é executada, a página passa por um ciclo de vida no qual
            ela executa uma série de etapas de processamento.
            <p style="text-indent: 36px">
                Elas incluem inicialização, criação de intancias de controles, restauração e manutenção
                de estado, execução dos eventos da página, processamento de código e renderização
                de controlesÉ importante entender o ciclo de vida da página para que você possa
                escrever o código no estágio do ciclo de vida apropriado para o efeito que pretende.
                Além disso, é possivel desenvolver controles personalizados, para isso deve estar
                familiarizado com o ciclo de vida da página para inicializar corretamente os controles,
                preencher propriedades de controle com exibição de dados de estado e executar qualquer
                lógica de comportamento do controle.
            </p>
        </div>
        <br />
        <b>O Asp.Net é interpretado ou compilado? Qual a diferença?</b>
        <hr />
        <div style="text-indent: 36px">
            <p style="text-indent: 36px">
                ASP.NET é compilado e não interpretado.
            </p>
            <p style="text-indent: 36px">
                ASP.NET bem como todas as aplicações .NET, são compiladas. É impossivel correr código
                C# ou VB sem que esteja compilado.
            </p>
            <p style="text-indent: 36px">
                Compilado - O compilador lê código numa linguagem e escreve código em outra linguagem.
            </p>
            <p style="text-indent: 36px">
                Interpretado - O interpretador lê código numa linguagem e faz (executa) o que o
                código manda.
            </p>
        </div>
        <br />
        <b>O que é uma variável de Sessão?</b>
        <hr />
        <p style="text-indent: 36px">
            As variáveis de sessão armazenam informações (normalmente parâmetros de formulário
            ou de URL enviados pelos utilizadores) e disponibilizam-nas a todas as páginas de
            um aplicativo da Web durante a sua visita.
        </p>
        <br />
        <b>O que é o Membership?</b>
        <hr />
        <p style="text-indent: 36px">
            O ASP.NET membership é uma funcionalidade built-in que valida e armazena as credenciais
            do utilizador. É especialmente utilizado em situações de login.
        </p>
        <br />
        <b>Em que consiste o ADO.NET?</b>
        <hr />
        <p style="text-indent: 36px">
            ADO.NET (ou a nova tecnologia ActiveX Data Objects) consiste num conjunto de classes
            definidas pela framework .NET (localizadas no namespace System.Data) que pode ser
            utilizado para aceder aos dados armazenados numa base de dados remota.
        </p>
        <br />
        <b>Qual a principal função das classes que a constituem?</b>
        <hr />
        <p style="text-indent: 36px">
            As classes que a constituem são importantes pois são elas que permitem a comunicação
            com a base de dados, a manipulação e gestão dos dados nela contidos.
        </p>
        <br />
        <b>Quais são as principais classes do ADO.NET e qual a sua função?</b>
        <hr />
        <div style="text-indent: 36px">
            <p style="text-indent: 0px; margin-left: 56px">
                ● <b>DataSet</b>: É uma representação em memória da base de dados. Contém uma ou
                mais DataTables;
                <br />
                ● <b>DataTable</b>: Representa uma tabela da base de dados. Contém uma ou mais DataRows;
                <br />
                ● <b>DataRow</b>: Representa um registo na tabela da base de dados. É um conjunto
                de campos de um registo.
            </p>
            <center>
                <br />
                <img id="Image2" alt="ADO .NET Data Architecture" src="../images/image00.png" />
            </center>
        </div>
        <br />
        <b>O que é uma MasterPage?</b>
        <hr />
        <div style="text-indent: 36px">
            <p style="text-indent: 36px">
                Uma MasterPage permite que seja criada uma aparência e comportamento consistentes
                para todas as páginas (ou grupo de páginas) na aplicação web.
            </p>
            <p style="text-indent: 36px">
                A MasterPage fornece um modelo para outras páginas, com layout e funcionalidade
                compartilhados. A página mestra define espaços reservados para o conteúdo, que pode
                ser substituído por páginas de conteúdo. O resultado de saída é uma combinação da
                página principal e a página de conteúdo, sendo que esta última contém o conteúdo
                que realmente se deseja mostrar.
            </p>
        </div>
        <br />
        <b>E um ContentPlaceHolder?</b>
        <hr />
        <p style="text-indent: 36px">
            Um ContentPlaceHolder é um espaço reservado dentro da masterpage onde se vai agregar
            o conteúdo ao layout, estando este na própria masterpage.
        </p>
        <br />
        <b>Consegues estabelecer alguma semelhança entre MasterPage e iFrame?</b>
        <hr />
        <div style="text-indent: 36px">
            <p style="text-indent: 36px">
                Sim, pois tanto numa como outra permitem a inclusão de um pedaço de informação como
                se de outra página completa se tratasse. Resumindo, é uma página secundária dentro
                de outra principal.
            </p>
            <p style="text-indent: 36px">
                No entanto, o iFrame pode ser utilizado em qualquer momento da página, diferindo
                da masterpage que apenas pode ser utilizado em conjunto com o ContentPlaceHolder.
            </p>
        </div>
        <br />
        <b>O que é um DataReader?</b>
        <hr />
        <p style="text-indent: 36px">
            O DataReader é uma classe que é utilizada para ler uma stream de dados de uma base
            de dados.
        </p>
        <br />
        <b>O que são Roles?</b>
        <hr />
        <p style="text-indent: 36px">
            Roles são papeis que podem ser atribuídos aos utilizadores. Estes Roles servem para
            dar níveis de acesso aos diferentes tipos de utilizadores a diferentes áreas do
            WebSite. Estes níveis normalmente são definidos pelo administrador principal. Por
            norma, são criadas duas Roles principais que distinguem utilizadores comuns de administradores.
        </p>
    </div>
</asp:Content>
