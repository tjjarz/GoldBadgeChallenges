using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Claims
{
    enum ValidClaimType { Car, Home, Theft }

    class Claim
    {
        public string ClaimID { get; set; }
        public ValidClaimType ClaimType { get; set; }
        public string Description { get; set; }
        public double amount { get; set; }
        public DateTime DateofIncident { get; set; }
        public DateTime DateofClaim { get; set; }
        public bool IsValid
        {
            get
            {
                TimeSpan timeSpan = DateofClaim - DateofIncident;
                if (timeSpan.TotalDays < 30)
                {
                    return true;
                }
                else { return false; }
            }

        }

        public Claim(string claimid, ValidClaimType claimtype, string desc, double ammount, DateTime incidentdate, DateTime claimdate)
        {
            ClaimID = claimid;
            ClaimType = claimtype;
            Description = desc;
            amount = ammount;
            DateofIncident = incidentdate;
            DateofClaim = claimdate;
        }

        public Claim() { }

        override public string ToString()
        {
            return $"{ClaimID}\t{ClaimType}\t{amount}\t{DateofIncident.ToShortDateString()}\t{DateofClaim.ToShortDateString()}\t{IsValid}\t{Description}";
        }

    }

    public class ClaimRepository
    {
        readonly Queue<Claim> _claims = new Queue<Claim>();

        //create
        public bool Add(Claim claim)
        {
            _claims.Enqueue(claim);
            return true;
        }
        //read

        public List<Claim> ReadAll(){return _claims.ToList();}
            
        public Queue<Claim> GetClaims(){return _claims;}

        public Claim Peek() { return _claims.Peek(); }
        
        public Claim Dequeue() {return _claims.Dequeue();}
        //update dont need update because Queue does ALL THAT for me :D
        
        //delete same here, I mean arguably I could 
        
        public void Filler()
        {
            _claims.Enqueue(new Claim("341", ValidClaimType.Car, "fender bender", 2000d, new DateTime(2020, 1, 10), DateTime.Now));
            _claims.Enqueue(new Claim("118", ValidClaimType.Home, "bender girder", 3000d, new DateTime(2020, 2, 10), DateTime.Now));
            _claims.Enqueue(new Claim("234", ValidClaimType.Home, "fender", 430d, new DateTime(2020, 2, 20), DateTime.Now));
            _claims.Enqueue(new Claim("998", ValidClaimType.Theft, "stratacaster", 6700d, new DateTime(2020, 1, 30), DateTime.Now));
            _claims.Enqueue(new Claim("7689", ValidClaimType.Theft, "fender bender", 4300d, new DateTime(2020, 1, 28), DateTime.Now));
            _claims.Enqueue(new Claim("5643", ValidClaimType.Car, "fender bender", 100d, new DateTime(2020, 1, 15), DateTime.Now));
            _claims.Enqueue(new Claim("2309", ValidClaimType.Home, "fender bender", 500d, new DateTime(2020, 1, 2), DateTime.Now));
            _claims.Enqueue(new Claim("9976", ValidClaimType.Car, "fender bender", 1300d, new DateTime(2020, 2, 15), DateTime.Now));
            _claims.Enqueue(new Claim("3", ValidClaimType.Theft, "fender bender", 1200d, new DateTime(2020, 2, 1), DateTime.Now));
        }
    }

}
