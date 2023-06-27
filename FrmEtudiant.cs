using GestionEtudiant.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionEtudiant
{
    public partial class FrmEtudiant : Form
    {
        dbetudiantEntities db;
        public FrmEtudiant()
        {
            InitializeComponent();
        }

        private void effacer()
        {
            txtNom.Text = string.Empty;
            txtPrenom.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtTel.Text = string.Empty;
            ccbGenre.Text = string.Empty;
            ccbFiliere.Text = string.Empty;
            txtNiveau.Text = string.Empty;
            dgvEtudiant.DataSource = db.Viewetudiant.ToList();
            txtNom.Focus();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void btn_quitter_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void FrmEtudiant_Load(object sender, EventArgs e)
        {
            // TODO: cette ligne de code charge les données dans la table 'dbetudiantDataSet2.Viewetudiant'. Vous pouvez la déplacer ou la supprimer selon les besoins.
            //this.viewetudiantTableAdapter1.Fill(this.dbetudiantDataSet2.Viewetudiant);
            // TODO: cette ligne de code charge les données dans la table 'dbetudiantDataSet.Viewetudiant'. Vous pouvez la déplacer ou la supprimer selon les besoins.
           // this.viewetudiantTableAdapter.Fill(this.dbetudiantDataSet.Viewetudiant);
            db = new dbetudiantEntities();
            dgvEtudiant.DataSource = db.Viewetudiant.ToList();

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Personne p = new Personne();
            p.NomPers = txtNom.Text;
            p.PrenomPers = txtPrenom.Text;
            p.TelPers = txtTel.Text;
            p.EmailPers = txtEmail.Text;
            db.Personne.Add(p);
            db.SaveChanges();
            Etudiant etu = new Etudiant();
            etu.idPers = p.idPers;
            etu.Genre = ccbGenre.Text;
            etu.Filiere = ccbFiliere.Text;
            etu.Niveau = txtNiveau.Text;
            db.Etudiant.Add(etu);
            db.SaveChanges();
            MessageBox.Show("ajouté evec succes");
            effacer();
        }

        private void btn_choisir_Click(object sender, EventArgs e)
        {
            txtNom.Text = dgvEtudiant.CurrentRow.Cells[1].Value.ToString();
            txtPrenom.Text = dgvEtudiant.CurrentRow.Cells[2].Value.ToString();
            txtTel.Text = dgvEtudiant.CurrentRow.Cells[3].Value.ToString();
            txtEmail.Text = dgvEtudiant.CurrentRow.Cells[4].Value.ToString();
            ccbGenre.Text = dgvEtudiant.CurrentRow.Cells[5].Value.ToString();
            ccbFiliere.Text = dgvEtudiant.CurrentRow.Cells[6].Value.ToString();
            txtNiveau.Text = dgvEtudiant.CurrentRow.Cells[7].Value.ToString();

        }

        private void txtNom_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_modifier_Click(object sender, EventArgs e)
        {
            int? id = int.Parse(dgvEtudiant.CurrentRow.Cells[0].Value.ToString());
            Personne p = db.Personne.Find(id);
            p.NomPers = txtNom.Text;
            p.PrenomPers = txtPrenom.Text;
            p.EmailPers = txtEmail.Text;
            p.TelPers = txtTel.Text;
            db.SaveChanges();
            Etudiant etu = db.Etudiant.Find(id);
            etu.Genre = ccbGenre.Text;
            etu.Filiere = ccbFiliere.Text;
            etu.Niveau = txtNiveau.Text;
            db.SaveChanges();
            MessageBox.Show("modification enregistrée");
            effacer();
        }

        private void btn_rechercher_Click(object sender, EventArgs e)
        {
            var liste = db.Viewetudiant.ToList();
            if (!string.IsNullOrEmpty(txtNom.Text))
            {
                liste = liste.Where(x => x.NomPers.ToUpper().Contains(txtNom.Text.ToUpper())).ToList();
            }
            if (!string.IsNullOrEmpty(txtPrenom.Text))
            {
                liste = liste.Where(x => x.PrenomPers.ToUpper().Contains(txtPrenom.Text.ToUpper())).ToList();
            }
            if (!string.IsNullOrEmpty(ccbGenre.Text))
            {
                liste = liste.Where(x => x.Genre == ccbGenre.Text).ToList();
            }
            dgvEtudiant.DataSource = liste.ToList();
        }

        private void btn_supprimer_Click(object sender, EventArgs e)
        {
            int? id = int.Parse(dgvEtudiant.CurrentRow.Cells[0].Value.ToString());
            Personne p = db.Personne.Find(id);
            db.Personne.Remove(p);
            db.SaveChanges();
            Etudiant etu = db.Etudiant.Find(id);
            db.Etudiant.Remove(etu);
            db.SaveChanges();
            MessageBox.Show("supprimé evec succes");
            effacer();
        }
    }
}
