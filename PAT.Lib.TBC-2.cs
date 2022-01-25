using System;
using System.Collections;
using System.Collections.Generic;
using PAT.Common.Classes.Expressions.ExpressionClass;
using System.Security.Cryptography;
using System.Text; 

namespace PAT.Lib {

    public class BManagementNode : ExpressionValue {
        public int mIdentifier;
        public double mDeposit;
        public double mNumActivities;
        public double mNumFailure;
        public double mWaitingTime;
        public double numParticipated = 0;
        public double numBProposed = 0;
        public int round;
        public double eDeposit;
       
        public BManagementNode() {
            mIdentifier = -1;
            mDeposit = 10;
            mNumActivities = 1;
            mWaitingTime = 1;
        }

        public BManagementNode(int _mIdentifier) {
            mIdentifier = _mIdentifier;
        }
        
        public BManagementNode(double _mDeposit, double _mNumActivities) {
            mDeposit = _mDeposit;
            mNumActivities = _mNumActivities;
        }
        
        public BManagementNode(int _mIdentifier, double _mDeposit, double _mNumActivities) {
            mIdentifier = _mIdentifier;
            mDeposit = _mDeposit;
            mNumActivities = _mNumActivities;
        }
        
        public BManagementNode(int _mIdentifier, double _mDeposit, double _mNumActivities, double _mWaitingTime) {
            mIdentifier = _mIdentifier;
            mDeposit = _mDeposit;
            mNumActivities = _mNumActivities;
            mWaitingTime = _mWaitingTime;
        }
        
        public BManagementNode(int _mIdentifier, double _mDeposit, double _mNumActivities, double _mNumFailure, double _mWaitingTime) {
            mIdentifier = _mIdentifier;
            mDeposit = _mDeposit;
            mNumActivities = _mNumActivities;
            mNumFailure = _mNumFailure;
            mWaitingTime = _mWaitingTime;
        }
        
        public int calculateITHash(int c1, int c2) {        	
        	SHA256 sha256Hash = SHA256.Create();
        	int intval = BitConverter.ToInt32(sha256Hash.ComputeHash(Encoding.UTF8.GetBytes("c1" + c2)), 0);
        	return intval;
    	}
        
        public void SetMIdentifier(int _mIdentifier) {
            mIdentifier = _mIdentifier;
        }

        public int GetMIdentifier() {
            return mIdentifier;
        }
        
        public void SetMDeposit(double _mDeposit) {
            mDeposit = _mDeposit;
        }

        public double GetMDeposit() {
            return mDeposit;
        }
        
        public double GetEscrow() {
            return eDeposit;
        }
        
        public void AddAmount(double _mDeposit) {
            mDeposit = this.mDeposit + _mDeposit;
        }
        
        public double SubmitMDeposit(double _deposit) {
            eDeposit = _deposit;
            return eDeposit;
        }
        
        public void SetNumBGAttempts(double _mNumActivities) {
            mNumActivities = _mNumActivities;
        }
        
        public void IncNumParticipation(double _numParticipated){
        	numParticipated = this.numParticipated + _numParticipated;
        }
        
        public double GetNumParticipation() {
            return numParticipated;
        }
        
        public double GetNumBProposed(){
        	return numBProposed;
        }
        
        public void SetNumBProposed(double _num){
        	numBProposed = numBProposed + _num;
        }
        
        public void IncNumBProposed(double _num){
        	numBProposed = numBProposed + _num;
        }
        
        public double GetNumBGAttempts() {
            return mNumActivities;
        }
        
        public void IncNumBGAttempts(double _mNumActivities) {
            mNumActivities = this.mNumActivities + _mNumActivities;
        }
        
        public double GetNumFailures() {
            return mNumFailure;
        }
        
        public void SetNumFailures(double f) {
            mNumFailure = f;
        }
        
        public void IncNumFailures(double f) {
            mNumFailure = mNumFailure + f;
        }
        
        public double GetWaitingTime() {
            return mWaitingTime;
        }
        
        public void SetWaitingTime(double _mWaitingTime) {
            mWaitingTime = _mWaitingTime;
        }
        
        public void IncWaitingTime(double _mWaitingTime) {
            mWaitingTime = this.mWaitingTime + _mWaitingTime;
        }
        
        public void SetRound(int _round) {
            round = _round;
        }

        public int GetRound() {
            return round;
        }
        
        public override string ToString() {
            return "[" + ExpressionID + "]";
        }
        
        public override ExpressionValue GetClone() {
            return new BManagementNode(mIdentifier);
        }
        
        public override string ExpressionID {
           get {
               return "BM" + mIdentifier;
           }
        }
	}
	
	public class BManagementNodes : ExpressionValue {
        public List<BManagementNode> bmlist;
        
        public BManagementNodes() {
            this.bmlist = new List<BManagementNode>();
        }

        public BManagementNodes(List<BManagementNode> _bmlist) {
            this.bmlist = _bmlist;            
        }
        
        public BManagementNode GetNode(int i) {
            return this.bmlist[i];
        }
        
        public double GetTotalDeposit() {
            double totalDeposit = 1;
            foreach (BManagementNode bm in bmlist) {
            	totalDeposit += bm.GetMDeposit();
            }
            return totalDeposit;
         }
         
         public double GetTNumBGAttempts() {
            double totalBGAttempts = 1;
            foreach (BManagementNode bm in bmlist) {
            	totalBGAttempts += bm.GetNumBGAttempts();
            }
            return totalBGAttempts;
         }
         
         public double GetTotalWaitingTime(){
         	double totalWaitingTime = 1;
         	foreach (BManagementNode bm in bmlist) {
            	totalWaitingTime += bm.GetWaitingTime();
            }
            return totalWaitingTime;
         }
        
        public void Add(BManagementNode _bmnode) {
            bool includes = false;
            foreach (BManagementNode bm in bmlist) {
                if(_bmnode.GetMIdentifier() == bm.GetMIdentifier()) {
                    includes = true;
                    break;
                }
            }
            if(!includes) {
                bmlist.Add(_bmnode);
            }
        }
        
        public int GetLength() {
            return this.bmlist.Count;
        }
        
        public override ExpressionValue GetClone() {
            return new BManagementNodes(new List<BManagementNode>(this.bmlist));
        }

        public override string ExpressionID {
            get {
                string returnString = "";
                foreach (BManagementNode bm in bmlist) {
                    returnString += bm.ToString() + ",";
                }
                return returnString;
               }
        }
        
        public override string ToString() {
            return "[" + ExpressionID + "]";
        } 
     }
     
     public class ValidatorNode : ExpressionValue {
        public int vIdentifier;
        public double vDeposit;
        public double vNumActivities;
        public double vNumFailure;
        public double vWaitingTime;
        public double numParticipated;
        public double numBValidated;
        public BManagementNode mNode;
        public WorkerNode wNode;
        public RequesterNode rNode;
        public double eDeposit;
       
        public ValidatorNode() {
            vIdentifier = -1;
            vDeposit = 0;
            vNumActivities = 0;
            vNumFailure = 0;
            vWaitingTime = 0;
        }

        public ValidatorNode(int _vIdentifier) {
            vIdentifier = _vIdentifier;
        }
        
        public ValidatorNode(double _vDeposit, double _vNumActivities) {
            vDeposit = _vDeposit;
            vNumActivities = _vNumActivities;
        }
        
        public ValidatorNode(int _vIdentifier, double _vDeposit, double _vNumActivities) {
            vIdentifier = _vIdentifier;
            vDeposit = _vDeposit;
            vNumActivities = _vNumActivities;
        }
        
        public ValidatorNode(int _vIdentifier, double _vDeposit, double _vNumActivities, double _vNumFailure, double _vWaitingTime, WorkerNode _wNode) {
            vIdentifier = _vIdentifier;
            vDeposit = _vDeposit;
            vNumActivities = _vNumActivities;
            vNumFailure = _vNumFailure;
            vWaitingTime = _vWaitingTime;
            wNode = _wNode;
        }
        
        public ValidatorNode(int _vIdentifier, double _vDeposit, double _vNumActivities, double _vNumFailure, double _vWaitingTime, RequesterNode _rNode) {
            vIdentifier = _vIdentifier;
            vDeposit = _vDeposit;
            vNumActivities = _vNumActivities;
            vNumFailure = _vNumFailure;
            vWaitingTime = _vWaitingTime;
            rNode = _rNode;
        }
        
        public ValidatorNode(int _vIdentifier, double _vDeposit, double _vNumActivities, double _vNumFailure, double _vWaitingTime, BManagementNode _mNode) {
            vIdentifier = _vIdentifier;
            vDeposit = _vDeposit;
            vNumActivities = _vNumActivities;
            vNumFailure = _vNumFailure;
            vWaitingTime = _vWaitingTime;
            mNode = _mNode;
        }
        
        public void AddAmount(double _vDeposit) {
            vDeposit = this.vDeposit + _vDeposit;
        }
        
        public void SetVIdentifier(int _vIdentifier) {
            vIdentifier = _vIdentifier;
        }

        public int GetVIdentifier() {
            return vIdentifier;
        }
        
        public double SubmitVDeposit(double _deposit) {
            eDeposit = _deposit;
            return eDeposit;
        }
        
        public double GetVEscrow(){
        	return eDeposit;
        }
        
        public void SetVDeposit(double _vDeposit) {
            vDeposit = _vDeposit;
        }

        public double GetVDeposit() {
            return vDeposit;
        }
        
        public double GetNumActivities() {
            return vNumActivities;
        }
        
        public void SetNumActivities(double _vNumActivities) {
            vNumActivities = _vNumActivities;
        }
        
        public void UpdateNumActivities(double _vNumActivities) {
            vNumActivities = this.vNumActivities + _vNumActivities;
        }
       
        public double GetNumBVAttempts() {
            return vNumActivities;
        }
       
       public void SetNumBVAttempts(double _vNumActivities) {
            vNumActivities = _vNumActivities;
        }
        
         public void IncNumBVAttempts(double _vNumActivities) {
            vNumActivities = _vNumActivities + 1;
        }
        
        public void IncNumParticipation(double _numParticipated){
        	numParticipated = this.numParticipated + _numParticipated;
        }
        
        public double GetNumParticipation() {
            return numParticipated;
        }
        
        public double GetNumBValidated(){
        	return numBValidated;
        }
        
        public void SetNumBValidated(double _numBValidated){
        	numBValidated = _numBValidated;
        }
        
        public void IncNumBValidated(double _num){
        	numBValidated = numBValidated + _num;
        }
        
        public double GetNumFailures() {
            return vNumFailure;
        }
        
        public void SetNumFailures(double f) {
            vNumFailure = f;
        }
        
        public void IncNumFailures(double f) {
            vNumFailure = vNumFailure + f;
        }
        
        public double GetWaitingTime() {
            return vWaitingTime;
        }
        
        public void SetWaitingTime(double _vWaitingTime) {
            vWaitingTime = _vWaitingTime;
        }
        
        public void IncWaitingTime(double _vWaitingTime) {
            vWaitingTime = this.vWaitingTime + _vWaitingTime;
        }
        
        public override string ToString() {
            return "[" + ExpressionID + "]";
        }

        public override ExpressionValue GetClone() {
            return new ValidatorNode(vIdentifier);
        }
        
        public override string ExpressionID {
           get {
               return "V" + vIdentifier;
           }
        }
	}
     
	public class ValidatorNodes : ExpressionValue {
        public List<ValidatorNode> vlset;
        
        public ValidatorNodes() {
            this.vlset = new List<ValidatorNode>();
        }

        public ValidatorNodes(List<ValidatorNode> vlset) {
            this.vlset = vlset;            
        }
        
        public ValidatorNode GetNode(int i) {
            return this.vlset[i];
        }
        
        public int GetLength() {
            return this.vlset.Count;
        }
        
        public void SetNode(int i, ValidatorNode _vl) {
            this.vlset[i] = _vl;
        }
        
        public void Nullify(ValidatorNodes _tvset){
        	_tvset = null; 
        }
        
        public double GetTotalDeposit() {
            double totalDeposit = 1;
            foreach (ValidatorNode vl in vlset) {
            	totalDeposit += vl.GetVDeposit();
            }
            return totalDeposit;
         }
         
         public double GetTotalNumActivities() {
            double totalNumActivities = 0;
            foreach (ValidatorNode vl in vlset) {
            	totalNumActivities += vl.GetNumActivities();
            }
            return totalNumActivities;
         }
        
        public double GetTNumBVAttempts() {
            double totalBVAttempts = 0;
            foreach (ValidatorNode vl in vlset) {
            	totalBVAttempts += vl.GetNumBVAttempts();
            }
            return totalBVAttempts;
         }
         
         public double GetTotalWaitingTime(){
         	double totalWaitingTime = 0;
         	foreach (ValidatorNode vl in vlset) {
            	totalWaitingTime += vl.GetWaitingTime();
            }
            return totalWaitingTime;
         }
        
        public void AddNode(ValidatorNode _vl) {
            bool includes = false;
            foreach (ValidatorNode vl in vlset) {
                if(_vl.GetVIdentifier() == vl.GetVIdentifier()) {
                    includes = true;
                    break;
                }
            }
            if(!includes) {
                vlset.Add(_vl);
            }
        }
        
        public void Empty(){
        	this.vlset.Clear();
        }
       
        public override ExpressionValue GetClone() {
            return new ValidatorNodes(new List<ValidatorNode>(this.vlset));
        }

        public override string ExpressionID {
            get {
                string returnString = "";
                foreach (ValidatorNode t in vlset) {
                    returnString += t.ToString() + ",";
                }
                return returnString;
               }
        }

        public override string ToString() {
            return "[" + ExpressionID + "]";
        } 
     }
     
     public class WorkerNode : ExpressionValue {
        public int wIdentifier;
        public double wDeposit;
        public double wNumActivities;
        public double wNumFailure;
        public double wWaitingTime;
       
        public WorkerNode() {
            wIdentifier = -1;
            wDeposit = 10;
            wNumActivities = 0;
            wNumFailure = 0;
            wWaitingTime = 0;
        }

        public WorkerNode(int _wIdentifier) {
            wIdentifier = _wIdentifier;
        }
        
        public WorkerNode(double _wDeposit, double _wNumActivities) {
            wDeposit = _wDeposit;
            wNumActivities = _wNumActivities;
        }
        
        public WorkerNode(int _wIdentifier, double _wDeposit, double _wNumActivities) {
            wIdentifier = _wIdentifier;
            wDeposit = _wDeposit;
            wNumActivities = _wNumActivities;
        }
        
        public WorkerNode(int _wIdentifier, double _wDeposit, double _wNumActivities, double _wNumFailure) {
            wIdentifier = _wIdentifier;
            wDeposit = _wDeposit;
            wNumActivities = _wNumActivities;
            wNumFailure = _wNumFailure;
        }
        
        public WorkerNode(int _wIdentifier, double _wDeposit, double _wNumActivities, double _wNumFailure, double _wWaitingTime) {
            wIdentifier = _wIdentifier;
            wDeposit = _wDeposit;
            wNumActivities = _wNumActivities;
            wNumFailure = _wNumFailure;
            wWaitingTime = _wWaitingTime;
        }
        
        public void SetWIdentifier(int _wIdentifier) {
            wIdentifier = _wIdentifier;
        }

        public int GetWIdentifier() {
            return wIdentifier;
        }
        
        public void SubmitWDeposit(double _wDeposit) {
            wDeposit = _wDeposit;
        }
        
        public void AddDeposit(double _wDeposit) {
            wDeposit = this.wDeposit + _wDeposit;
        }

        public double GetDeposit() {
            return wDeposit;
        }
        
        public void SetNumActivities(double _wNumActivities) {
            wNumActivities = _wNumActivities;
        }
        
        public void IncNumActivities(double _wNumActivities) {
            wNumActivities = this.wNumActivities + _wNumActivities;
        }
        
        public double GetNumActivities() {
            return wNumActivities;
        }
      
        public override string ToString() {
            return "[" + ExpressionID + "]";
        }

        public override ExpressionValue GetClone() {
            return new WorkerNode(wIdentifier);
        }
        
        public override string ExpressionID {
           get {
               return "W" + wIdentifier;
           }
        }
	}
	
	public class WorkerNodes : ExpressionValue {
        public List<WorkerNode> wnset;
        
        public WorkerNodes() {
            this.wnset = new List<WorkerNode>();
        }

        public WorkerNodes(List<WorkerNode> _wnset) {
            this.wnset = _wnset;            
        }
        
        public WorkerNode GetNode(int i) {
            return this.wnset[i];
        }
        
        public int GetLength() {
            return this.wnset.Count;
        }
        
        public void SetNode(int i, WorkerNode _wnode) {
            this.wnset[i] = _wnode;
        }
        
        public void Add(WorkerNode _wnode) {
            bool includes = false;
            foreach (WorkerNode wn in wnset) {
                if(_wnode.GetWIdentifier() == wn.GetWIdentifier()) {
                    includes = true;
                    break;
                }
            }
            if(!includes) {
                wnset.Add(_wnode);
            }
        }
       
        public override ExpressionValue GetClone() {
            return new WorkerNodes(new List<WorkerNode>(this.wnset));
        }

        public override string ExpressionID {
            get {
                string returnString = "";
                foreach (WorkerNode wn in wnset) {
                    returnString += wn.ToString() + ",";
                }
                return returnString;
               }
        }
        
        public override string ToString() {
            return "[" + ExpressionID + "]";
        } 
     }
	
	public class RequesterNode : ExpressionValue {
        public int rIdentifier;
        public double rDeposit;
        public double rNumActivities;
        public double rNumFailure;
        public double rWaitingTime;
       
        public RequesterNode() {
            rIdentifier = -1;
            rDeposit = 0;
            rNumActivities = 0;
            rNumFailure = 0; 
            rWaitingTime = 0;
        }

        public RequesterNode(int _rIdentifier) {
            rIdentifier = _rIdentifier;
        }
        
        public RequesterNode(int _rDeposit, double _rNumActivities) {
            rDeposit = _rDeposit;
            rNumActivities = _rNumActivities;
        }
        
        public RequesterNode(int _rIdentifier, double _rDeposit, double _rNumActivities) {
            rIdentifier = _rIdentifier;
            rDeposit = _rDeposit;
            rNumActivities = _rNumActivities;
        }
        
        public RequesterNode(int _rIdentifier, double _rDeposit, double _rNumActivities, double _rWaitingTime) {
            rIdentifier = _rIdentifier;
            rDeposit = _rDeposit;
            rNumActivities = _rNumActivities;
            rWaitingTime = _rWaitingTime;
        }
        
        public RequesterNode(int _rIdentifier, double _rDeposit, double _rNumActivities, double _rNumFailure, double _rWaitingTime) {
            rIdentifier = _rIdentifier;
            rDeposit = _rDeposit;
            rNumActivities = _rNumActivities;
            rNumFailure = _rNumFailure;
            rWaitingTime = _rWaitingTime;
        }
        
        
        public void SetRIdentifier(int _rIdentifier) {
            rIdentifier = _rIdentifier;
        }

        public int GetRIdentifier() {
            return rIdentifier;
        }
        
        public double SubmitRDeposit(double _rDeposit) {
            rDeposit = _rDeposit;
            return rDeposit;
        }
        
        public void AddDeposit(double _rDeposit) {
            rDeposit = this.rDeposit + _rDeposit;
        }

        public double GetRDeposit() {
            return rDeposit;
        }
        
        public void SetNumActivities(double _rNumActivities) {
            rNumActivities = _rNumActivities;
        }
        
        public void UpdateNumActivities(double _rNumActivities) {
            rNumActivities = this.rNumActivities + _rNumActivities;
        }
        
        public double GetNumActivities() {
            return rNumActivities;
        }
       
        public override string ToString() {
            return "[" + ExpressionID + "]";
        }

        public override ExpressionValue GetClone() {
            return new RequesterNode(rIdentifier);
        }

        public override string ExpressionID {
           get {
               return "R" + rIdentifier;
           }
        }
	}
	
	public class RequesterNodes : ExpressionValue {
        public List<RequesterNode> rnset;
        
        public RequesterNodes() {
            this.rnset = new List<RequesterNode>();
        }

        public RequesterNodes(List<RequesterNode> _rnset) {
            this.rnset = _rnset;            
        }
        
        public RequesterNode Get(int i) {
            return this.rnset[i];
        }
        
        public int GetLength() {
            return this.rnset.Count;
        }
        
        public void Set(int i, RequesterNode _rnode) {
            this.rnset[i] = _rnode;
        }
        
        public void Add(RequesterNode _rnode) {
            bool includes = false;
            foreach (RequesterNode r in rnset) {
                if(_rnode.GetRIdentifier() == r.GetRIdentifier()) {
                    includes = true;
                    break;
                }
            }
            if(!includes) {
                rnset.Add(_rnode);
            }
        }
 
        public override ExpressionValue GetClone() {
            return new RequesterNodes(new List<RequesterNode>(this.rnset));
        }
 
        public override string ExpressionID {
            get {
                string returnString = "";
                foreach (RequesterNode r in rnset) {
                    returnString += r.ToString() + ",";
                }
                return returnString;
               }
        }

        public override string ToString() {
            return "[" + ExpressionID + "]";
        } 
     }
     
    public class Transaction : ExpressionValue {
    	public string fromAdd;
    	public string toAdd;
    	public int tAmount;
    	public int tvotes;
    	public int tHash;
    	
    	public Transaction() {
    		
    	}
    	
    	public Transaction(int _tvotes) {
    		this.tvotes = _tvotes;
    	}
    	
    	public Transaction(string _fromAdd, string _toAdd, int _tAmount) {
    		this.fromAdd = _fromAdd;
    		this.toAdd = _toAdd;
    		this.tAmount = _tAmount;
    		this.tvotes = 0;
    	}
    	
    	public int GetTVotes(){
        	return tvotes;
        }
        
        public void IncTVotes(int _tvotes){
        	tvotes = this.tvotes + _tvotes;
        }
        
        public void SetTVotes(int _tvotes){
        	tvotes = this.tvotes + _tvotes;
        }
        
        public int getTHash(){
        return tHash;
        }
    	
    	public override string ToString() {
            return "[" + ExpressionID + "]";
        }
        
        public override ExpressionValue GetClone() {
            return new Transaction(fromAdd, toAdd, tAmount);
        }
        
        public override string ExpressionID {
           get {
               return "T" + fromAdd + toAdd + tAmount;
           }
        }
    }
    
    public class Transactions : ExpressionValue {
        public List<Transaction> tList;
        
        public Transactions() {
            this.tList = new List<Transaction>();
        }

        public Transactions(List<Transaction> _tList) {
            this.tList = _tList;            
        }
        
        public void Add(Transaction _tran){
        	this.tList.Add(_tran);
        }
        
        public Transaction Get(int i) {
            return this.tList[i];
        }
         
		private readonly Random _random = new Random();       
		public int RandomNumber(int min, int max)  
		{  
  			return _random.Next(min, max);  
		}  
        
        public void Set(int i, Transaction _tran) {
            this.tList[i] = _tran;
        }
        
        public int GetLength() {
            return this.tList.Count;
        }
     
        public override ExpressionValue GetClone() {
            return new Transactions(new List<Transaction>(this.tList));
        }

        public override string ExpressionID {
            get {
                string returnString = "";
                foreach (Transaction tl in tList) {
                    returnString += tl.ToString() + ",";
                }
                return returnString;
               }
        }
 
        public override string ToString() {
            return "[" + ExpressionID + "]";
        } 
     } 
     
     public class Block : ExpressionValue {
        public int ind;
        public int bHash;
        public int prevBlockHash;
        public Transactions pendingTransactions;
        public BSigs bsigs;
        public BManagementNode leader;
       
        public Block() {
            bHash = -1;
            bsigs = new BSigs();
        }

        public Block(int _bHash) {
            bHash = _bHash;
            bsigs = new BSigs();
        }

        public Block(int _bHash, BSigs _bsigs) {
            bHash = _bHash;
            bsigs = new BSigs(new List<BSig>(_bsigs.GetList()));
        }
        
        public Block(int _ind, BManagementNode _leader, int _prevBlockHash, Transactions _pendingTransactions) {
            this.ind = _ind;
            this.leader = _leader;
            this.prevBlockHash = _prevBlockHash;
            this.bHash = this.calculateBHash();
            this.pendingTransactions = _pendingTransactions;
            bsigs = new BSigs();
        }

        public void SetSignatureList(BSigs _bsigs) {
            bsigs = new BSigs(new List<BSig>(bsigs.GetList()));
        }

        public BSigs GetSignatureList() {
            return bsigs;
        }
        
        public void SetHash(int _bHash) {
            bHash = _bHash;
        }

        public int GetBlockHash() {
            return bHash;
        }
        
        public int GetPrevHash() {
            return prevBlockHash;
        }
        
        public Transactions GetTransactions() {
            return pendingTransactions;
        }
        
        public int calculateBHash() {        	
        	SHA256 sha256Hash = SHA256.Create();
        	int bytes = BitConverter.ToInt32(sha256Hash.ComputeHash(Encoding.UTF8.GetBytes("this.index" + this.leader + this.prevBlockHash + this.pendingTransactions)), 0);
        	return bytes;
    	}
    	
    	public int GetIndex(){
    		return ind;
    	}
    	
    	public BManagementNode GetLeader(){
    		return leader;
    	}

        public override string ToString() {
            return "[" + ExpressionID + "]";
        }

        public override ExpressionValue GetClone() {
            return new Block(bHash, new BSigs(new List<BSig>(bsigs.GetList())));
        }

        public override string ExpressionID {
           get {
               return "B" + bHash;
           }
        }
    }
    
    public class Blocks : ExpressionValue {
        public List<Block> blist;
        
        public Blocks() {
            this.blist = new List<Block>();
        }

        public Blocks(List<Block> _blist) {
            this.blist = _blist;            
        }
        
        public void Set(int index, Block _block) {
            while(index >= blist.Count) {
               this.blist.Add(new Block()); 
            }
            this.blist[index] = _block;
        }

        public Block Get(int index) {
            if(index >= blist.Count) {
                return new Block();
            }
            return this.blist[index];
        }

        public void Clear() {
            this.blist.Clear();
        }
        
        public override ExpressionValue GetClone() {
            return new Blocks(new List<Block>(this.blist));
        }

        public override string ExpressionID {
            get {
                string returnString = "";
                foreach (Block b in blist) {
                    returnString += b.ToString() + ",";
                }
                return returnString;
               }
        }

        public override string ToString() {
            return "[" + ExpressionID + "]";
        }
    }
     
     public class Blockchain : ExpressionValue {
        public List<Block> blockchain;
        public Transactions pendingTransactions;
        public int mReward;
        public int escrowAmount;
        
        public Blockchain() {
            this.blockchain = new List<Block>();
            this.pendingTransactions = null;   
        }

        public Blockchain(List<Block> _blockchain) {
            this.blockchain = _blockchain;            
        }
        
        public Block CreateGenesisBlock(){
    		return new Block();
    	}
        
        public Block GetHeadBlock() {
            if(this.blockchain.Count == 0) {
                return new Block();
            }
            return this.blockchain[this.blockchain.Count-1];
        }
        
        public BManagementNode GetBlockLeader(Block _block){
        	return _block.GetLeader();
        }
        
        public Block GetRandomBlock(int ran) {
            
            return this.blockchain[ran];
        }
        
        private readonly Random _random = new Random();       
		public int RandomNumber(int min, int max)  
		{  
  			return _random.Next(min, max);  
		}  
        
        public int GetHeight() {
            return this.blockchain.Count;
        }
        
        public void AddBlock(Block _block) {
            this.blockchain.Add(_block);
        }
        
        public void SetEAmount(int amount) {
            escrowAmount = this.escrowAmount + amount;
        }
        
        public void RemoveEAmount(int amount) {
            escrowAmount = this.escrowAmount - amount;
        }
        
        public bool IncludesBlock(Block _block) {
            foreach (Block b1 in blockchain) {
                if(b1.GetBlockHash() == _block.GetBlockHash()) {
                    return true;
                }
            }
            return false;
        }

        public bool ContainsInvalidBlocks() {
            List<Block> tempList = new List<Block>();
            foreach (Block bk in blockchain) {
                if (!tempList.Contains(bk)) {
                    tempList.Add(bk);
                } else {
                    return true;
                }
            }
            return false;
        }

        public Block GetBlock(int index) {
            return this.blockchain[index];
        }
        
        public bool IsEmpty() {
            return this.blockchain.Count == 0;
        }
        
        public override ExpressionValue GetClone() {
            return new Blockchain(new System.Collections.Generic.List<Block>(this.blockchain));
        }

        public override string ExpressionID {
            get {
                string returnString = "";
                foreach (Block b in blockchain) {
                    returnString += b.GetBlockHash() + ",";
                }
                return returnString;
            }
        }
        
        public override string ToString() {
            return "[" + ExpressionID + "]";
        }
    }
    
    public class BSig: ExpressionValue {
        public int bsig;
        
        public BSig() {
            bsig = -1;
        }

        public BSig(int _bsig) {
            bsig = _bsig;
        }
        
        public BSig(BSig _bsig) {
            bsig = _bsig.GetBSignature();
        }
        
        public void SetBSignature(int _bsig) {
            bsig = _bsig;
        }

        public int GetBSignature() {
            return bsig;
        }
        
        public override string ToString() {
            return bsig.ToString();
        }

        public override ExpressionValue GetClone() {
            return new BSig(bsig);
        }
 
        public override string ExpressionID {
           get {
               return bsig.ToString();
           }
        }
    }
    
    public class BSigs : ExpressionValue {
        public List<BSig> slist;
        
        public BSigs() {
            this.slist = new List<BSig>();
        }

        public BSigs(List<BSig> _slist) {
            this.slist = _slist;            
        }

        public List<BSig> GetList() {
            return this.slist;
        }

        public override ExpressionValue GetClone() {
            return new BSigs(new List<BSig>(this.slist));
        }
 
        public override string ExpressionID {
            get {
                string returnString = "";
                foreach (BSig s in slist) {
                    returnString += s.ToString() + ",";
                }
                return returnString;
               }
        }

        public override string ToString() {
            return "[" + ExpressionID + "]";
        }
    }
    
    public class PendingBlock : ExpressionValue {
        public Block block;
        public BSig bsig;
        
        public PendingBlock() {
            block = new Block();
            bsig = new BSig();
        }

        public PendingBlock(Block b, BSig _bsig) {
            block = new Block(b.GetBlockHash());
            bsig = new BSig(_bsig);
        }

        public Block GetBlock() {
            return block;
        }
        
        public int GetSignature() {
            return bsig.GetBSignature();
        }

        public override ExpressionValue GetClone() {
            return new PendingBlock(block, bsig);
        }

        public override string ExpressionID {
           get {
               return "PendingBlocks [PB" + block.GetBlockHash() + " by L" + bsig.ToString() + "]";
           }
        }

        public override string ToString() {
            return ExpressionID;
        }
    }
    
    public class PendingBlocks : ExpressionValue {
        public List<PendingBlock> bplist;
        
        public PendingBlocks() {
            this.bplist = new List<PendingBlock>();
        }

        public PendingBlocks(List<PendingBlock> _bplist) {
            this.bplist = _bplist;            
        }
        
        public void SetPBlock(int index, PendingBlock bproposal) {
            while(index >= bplist.Count) {
               this.bplist.Add(new PendingBlock()); 
            }
            this.bplist[index] = bproposal;
        }

        public PendingBlock GetPBlock(int index) {
            return this.bplist[index];
        }

        public override ExpressionValue GetClone() {
            return new PendingBlocks(new List<PendingBlock>(this.bplist));
        }

        public override string ExpressionID {
            get {
                string returnString = "";
                foreach (PendingBlock p in bplist) {
                    returnString += p.ToString() + ",";
                }
                return returnString;
               }
        }
        
        public override string ToString() {
            return "[" + ExpressionID + "]";
        }
    } 
    
    public class BVote : ExpressionValue, IComparable {
        public int blockHash;
        public BSig bsig;
        public int totalVotes;

        public BVote() {
            blockHash = -1;
            bsig = new BSig();
            //totalVotes = 0;
        }

        public BVote(int bh, BSig _bsig) {
            blockHash = bh;
            bsig = new BSig(_bsig);
        }
        
        public BVote(int bh, BSig _bsig, int _vote) {
            blockHash = bh;
            bsig = new BSig(_bsig);
            this.totalVotes = this.totalVotes + _vote;
        }

        public int GetBlockHash() {
            return blockHash;
        }
        
        public int GetSignature() {
            return bsig.GetBSignature();
        }
        
        public void updateVotes(int _totalVotes){
        	this.totalVotes = this.totalVotes + _totalVotes;
        	//return totalVotes;
        }
        
        public int GetVotes(){
        	return this.totalVotes; 
        }
        
        public void Clear() {
            totalVotes = 0;
        }

        int IComparable.CompareTo(object bobj) {
            BVote bv = (BVote) bobj;
            return String.Compare(this.ToString(), bv.ToString());
        }

        public override ExpressionValue GetClone() {
            return new BVote(blockHash, bsig, totalVotes);
        }

        public override string ExpressionID {
           get {
               return "Vote [BV" + blockHash + " by L" + bsig.ToString() + "]";
           }
        }

        public override string ToString() {
            return ExpressionID;
        }
    }
    
    public class BVoteSet : ExpressionValue {
        public SortedList<BVote> bset;
        public Dictionary<int, int> bcounter;

        public BVoteSet() {
            this.bset = new SortedList<BVote>();
            this.bcounter = new Dictionary<int, int>();
        }

        public BVoteSet(SortedList<BVote> _bset, Dictionary<int, int> _bcounter) {
            this.bset = _bset;
            this.bcounter = _bcounter;
        }
        
        public int Size() {
            return this.bset.Count;
        }

        public void Clear() {
            bset.Clear();
            bcounter.Clear();
        }

        public void Add(BVote bvote) {
            bool contains = false;
            foreach (BVote bv in bset) {
                if(bvote.GetBlockHash() == bv.GetBlockHash() && bvote.GetSignature() == bv.GetSignature()) {
                    contains = true;
                    break;
                }
            }
            if(!contains) {
                bset.Add(bvote);
                if(bcounter.ContainsKey(bvote.GetBlockHash())) {
                    bcounter[bvote.GetBlockHash()]++;
                } else {
                    bcounter.Add(bvote.GetBlockHash(), 1);
                }
            }
        }

        public BSigs getSignaturesForBlock(int blockHash) {
            List<BSig> bsigs = new List<BSig>();
            foreach (BVote bv in bset) {
                if(bv.GetBlockHash() == blockHash) {
                    bsigs.Add(new BSig(bv.GetSignature()));
                }
            }
            return new BSigs(new List<BSig>(bsigs));
        }

        public Block BlockWithMajority(int bmin) {
            foreach(KeyValuePair<int, int> bentry in bcounter) {
                if(bentry.Value >= bmin) {
                    return new Block(bentry.Key);
                }
            }
            return new Block();
        }

        public override ExpressionValue GetClone() {
            return new BVoteSet(new SortedList<BVote>(new List<BVote>(this.bset.GetInternalList())), new Dictionary<int, int>(this.bcounter));
        }

        public override string ExpressionID {
            get {
                string returnString = "";
                foreach (KeyValuePair<int, int> bkvp in bcounter) {
                    returnString += string.Format("{0}:{1};", bkvp.Key, bkvp.Value);
                }
                foreach (BVote bv in bset) {
                    returnString += bv.ToString() + ",";
                }
                return returnString;
            }
        }

        public override string ToString() {
            return "[" + ExpressionID + "]";
        }
    }
    
 public class SortedList<T> : ICollection<T> {
        private List<T> m_innerList;
        private Comparer<T> m_comparer;
    
        public SortedList() : this(new List<T>()) {}
        
        public SortedList(List<T> m_innerList) {
            this.m_innerList = m_innerList;
            this.m_comparer = Comparer<T>.Default;
        }
        
        public List<T> GetInternalList() {
            return  m_innerList;
        }
    
        public void Add(T item) {
            int insertIndex = FindIndexForSortedInsert(m_innerList, m_comparer, item);
            m_innerList.Insert(insertIndex, item);
        }
    
        public bool Contains(T item) {
            return IndexOf(item) != -1;
        }
    
        public int IndexOf(T item) {
            int insertIndex = FindIndexForSortedInsert(m_innerList, m_comparer, item);
            if (insertIndex == m_innerList.Count) {
                return -1;
            }
            if (m_comparer.Compare(item, m_innerList[insertIndex]) == 0) {
                int index = insertIndex;
                while (index > 0 && m_comparer.Compare(item, m_innerList[index - 1]) == 0) {
                    index--;
                }
                return index;
            }
            return -1;
        }
    
        public bool Remove(T item) {
            int index = IndexOf(item);
            if (index >= 0) {
                m_innerList.RemoveAt(index);
                return true;
            }
            return false;
        }
    
        public void RemoveAt(int index) {
            m_innerList.RemoveAt(index);
        }
    
        public void CopyTo(T[] array) {
            m_innerList.CopyTo(array);
        }
    
        public void CopyTo(T[] array, int arrayIndex) {
            m_innerList.CopyTo(array, arrayIndex);
        }
    
        public void Clear() {
            m_innerList.Clear();
        }
    
        public T this[int index] {
            get {
                return m_innerList[index];
            }
        }
    
        public IEnumerator<T> GetEnumerator() {
            return m_innerList.GetEnumerator();
        }
    
        IEnumerator IEnumerable.GetEnumerator() {
            return m_innerList.GetEnumerator();
        }
    
        public int Count {
            get {
                return m_innerList.Count;
            }
        }
    
        public bool IsReadOnly {
            get {
                return false;
            }
        }
    
        public static int FindIndexForSortedInsert(List<T> list, Comparer<T> comparer, T item) {
            if (list.Count == 0) {
                return 0;
            }
    
            int lowerIndex = 0;
            int upperIndex = list.Count - 1;
            int comparisonResult;
            while (lowerIndex < upperIndex) {
                int middleIndex = (lowerIndex + upperIndex) / 2;
                T middle = list[middleIndex];
                comparisonResult = comparer.Compare(middle, item);
                if (comparisonResult == 0) {
                    return middleIndex;
                } else if (comparisonResult > 0) {  
                    upperIndex = middleIndex - 1;
                } else {    
                    lowerIndex = middleIndex + 1;
                }
            }
    
            comparisonResult = comparer.Compare(list[lowerIndex], item);
            if (comparisonResult < 0) {
                return lowerIndex + 1;
            } else {
                return lowerIndex;
            }
        }
    }
}    