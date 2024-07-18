using System.Net.Sockets;

namespace teamview
{
    public partial class Form1 : Form
    {
        TcpClient tcpClient = null;
        TcpListener tcpListener = null;
        NetworkStream newWorkStream = null;


        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtIP.Text.Trim() == string.Empty
                    || txtIPCLient.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Input can not be Empty!");
                }

                tcpClient = new TcpClient(txtIPCLient.Text, 8080);
                newWorkStream = tcpClient.GetStream();
                MessageBox.Show("Success");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", ex.Message);

            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (tcpClient != null && tcpClient.Connected) // kiem tra xem cos client ket noi chua
                {
                    newWorkStream.Close();
                    tcpClient.Close();
                    tcpClient = null;//gan null
                    newWorkStream = null; //gan null
                    MessageBox.Show("Disconnected");

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", ex.Message);

            }
        }

        private void InitialTcpListener()
        {
            try
            {
                tcpListener = new TcpListener(System.Net.IPAddress.Any, 8080);
                tcpListener.Start();
                tcpListener.BeginAcceptTcpClient(new AsyncCallback(WhenClientConnect), null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", ex.Message);

            }
        }

        private void WhenClientConnect(IAsyncResult rs)
        {
            try
            {
                tcpClient = tcpListener.EndAcceptTcpClient(rs);
                newWorkStream = tcpClient.GetStream();
                MessageBox.Show("Connected", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                tcpListener.BeginAcceptTcpClient(new AsyncCallback(WhenClientConnect), null);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitialTcpListener();
        }
    }
}
