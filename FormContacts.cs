using ContactsApp.Entity;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContactsApp
{
    public partial class Contacts : Form
    {
        private Trie trie;

        public Contacts()
        {
            InitializeComponent();

            trie = new Trie();

            try
            {
                List<KeyValuePair<string, string>> contacts = new List<KeyValuePair<string, string>>();
                contacts.Add(new KeyValuePair<string, string>("Adailia", "398475983"));
                contacts.Add(new KeyValuePair<string, string>("Adair", "98437598"));
                contacts.Add(new KeyValuePair<string, string>("Adaire", "641846451"));
                contacts.Add(new KeyValuePair<string, string>("Aase", "57545755"));
                contacts.Add(new KeyValuePair<string, string>("Aaries", "85549874"));
                contacts.Add(new KeyValuePair<string, string>("Aba", "78545645"));
                contacts.Add(new KeyValuePair<string, string>("Abagail", "7555784"));
                contacts.Add(new KeyValuePair<string, string>("Badal", "8446464"));
                contacts.Add(new KeyValuePair<string, string>("Baedin", "785547646"));
                contacts.Add(new KeyValuePair<string, string>("Balthazar", "8945748645"));
                contacts.Add(new KeyValuePair<string, string>("Banefre", "8945864"));
                contacts.Add(new KeyValuePair<string, string>("Earle", "89684879"));
                contacts.Add(new KeyValuePair<string, string>("Early", "9844684"));
                contacts.Add(new KeyValuePair<string, string>("Edson", "84557984"));
                contacts.Add(new KeyValuePair<string, string>("Efrem", "5475554"));
                contacts.Add(new KeyValuePair<string, string>("Eglamour", "89431654"));
                contacts.Add(new KeyValuePair<string, string>("Efi", "4564985"));
                contacts.Add(new KeyValuePair<string, string>("Hais", "784486798"));
                contacts.Add(new KeyValuePair<string, string>("Haiz", "84545484"));
                contacts.Add(new KeyValuePair<string, string>("Haize", "84548956"));
                contacts.Add(new KeyValuePair<string, string>("Hakaku", "84514599"));
                contacts.Add(new KeyValuePair<string, string>("Hallam", "84549898"));
                contacts.Add(new KeyValuePair<string, string>("Kala", "84318979"));
                contacts.Add(new KeyValuePair<string, string>("Kalama", "984557"));
                contacts.Add(new KeyValuePair<string, string>("Kalyn", "687146864"));
                contacts.Add(new KeyValuePair<string, string>("Kaida", "92816149"));
                contacts.Add(new KeyValuePair<string, string>("Kaitey", "68869144"));
                contacts.Add(new KeyValuePair<string, string>("Oakie", "87181416"));
                contacts.Add(new KeyValuePair<string, string>("Oaklee", "5846146"));
                contacts.Add(new KeyValuePair<string, string>("Oakleigh", "581487485"));
                contacts.Add(new KeyValuePair<string, string>("Ocean", "68468486"));
                contacts.Add(new KeyValuePair<string, string>("Oceana", "5754689"));
                contacts.Add(new KeyValuePair<string, string>("Olimpi", "846859"));
                contacts.Add(new KeyValuePair<string, string>("Olina", "5584515"));
                contacts.Add(new KeyValuePair<string, string>("Olinda", "5548648"));
                contacts.Add(new KeyValuePair<string, string>("Saadya", "84644"));
                contacts.Add(new KeyValuePair<string, string>("Saadyah", "654546186"));
                contacts.Add(new KeyValuePair<string, string>("Saahdia", "5454858"));
                contacts.Add(new KeyValuePair<string, string>("Sander", "61684186"));
                contacts.Add(new KeyValuePair<string, string>("Sanders", "581455"));
                contacts.Add(new KeyValuePair<string, string>("Daelin", "866166"));
                contacts.Add(new KeyValuePair<string, string>("Daelon", "158454"));
                contacts.Add(new KeyValuePair<string, string>("Daelyn", "1848668"));
                contacts.Add(new KeyValuePair<string, string>("Dakottah", "61984646"));
                contacts.Add(new KeyValuePair<string, string>("Dal", "193861155"));
                contacts.Add(new KeyValuePair<string, string>("Vahe", "19916486"));
                contacts.Add(new KeyValuePair<string, string>("Vaibhav", "1979924681"));
                contacts.Add(new KeyValuePair<string, string>("Xenick", "64876878"));
                contacts.Add(new KeyValuePair<string, string>("Xenik", "68764164"));
                contacts.Add(new KeyValuePair<string, string>("Xeno", "584848"));
                contacts.Add(new KeyValuePair<string, string>("Xenoes", "6868413"));
                contacts.Add(new KeyValuePair<string, string>("Zacharyah", "871231654"));
                contacts.Add(new KeyValuePair<string, string>("Zachriel", "84848451"));
                contacts.Add(new KeyValuePair<string, string>("Zack", "68487895"));
                contacts.Add(new KeyValuePair<string, string>("Zenny", "587752132"));
                contacts.Add(new KeyValuePair<string, string>("Zeno", "8845815"));
                contacts.Add(new KeyValuePair<string, string>("Zanoah", "98764351"));


                foreach (KeyValuePair<string, string> contact in contacts)
                {
                    trie.insert(contact.Key, contact.Value);
                }
            }
            catch (Exception)
            {
                // log error
            }
            searchContactsByPrefix();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            searchContactsByPrefix();
            //txtSearch.Text = "Search";
        }

        private void searchContactsByPrefix()
        {
            try
            {
                listViewContacts.Items.Clear();
                List<KeyValuePair<string, object>> searchResult = trie.prefixSearch(txtSearch.Text);
                if (searchResult != null && searchResult.Count != 0)
                {
                    listViewContacts.Items.AddRange(searchResult.Select(result => new ListViewItem(result.Key + " (" + result.Value.ToString() + ")")).ToArray());
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurred while feching contacts");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string contactName = Interaction.InputBox("Please enter contact name", "Contact Input", null);
                if (!string.IsNullOrEmpty(contactName))
                {
                    //Regex re = new Regex(@"^[a-z]+$");
                    if (Regex.IsMatch(contactName, @"^[a-z]+$"))
                    {
                        string contactNumber = Interaction.InputBox("Please enter contact number", "Contact Input", null);
                        if (Regex.IsMatch(contactNumber, @"^[0-9]+$"))
                        {
                            if (contactNumber == null)
                            {
                                MessageBox.Show("Cannot add empty contact number");
                            }
                            else
                            {
                                if (trie.insert(contactName, contactNumber))
                                {
                                    MessageBox.Show("Contact added successfully");
                                }
                                else
                                {
                                    MessageBox.Show("Contact already exists");
                                }
                            } 
                        }
                        else
                        {
                            MessageBox.Show("Contact number should contain digits only");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Contact name should only contain small letter alphabets");
                    }
                }
                else
                {
                    MessageBox.Show("Cannot add empty contact name");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurred while adding contact");
            }
            searchContactsByPrefix();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool deletedAll = true;
            try
            {
                if (listViewContacts.SelectedItems != null && listViewContacts.SelectedItems.Count != 0)
                {
                    foreach (ListViewItem listViewItem in listViewContacts.SelectedItems)
                        try
                        {
                            if (!trie.delete(listViewItem.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray()[0]))
                            {
                                deletedAll = false;
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Error occurred while deleting contact " + listViewItem.Text);
                        }
                    if(deletedAll)
                        MessageBox.Show("Contact(s) deleted successfully");
                    else
                        MessageBox.Show("Could not delete one or more contacts");
                }
                else
                {
                    MessageBox.Show("Select contact(s) to be deleted");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurred while deleting contact");
            }
            searchContactsByPrefix();
        }
    }
}
