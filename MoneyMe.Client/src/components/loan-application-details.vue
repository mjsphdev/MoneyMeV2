<script setup lang="ts">
import VueSlider from 'vue-slider-component'
import 'vue-slider-component/theme/antd.css'
import { ref } from 'vue';
import { useRoute } from 'vue-router';
import axios from "axios";
import { dateFormat } from '@/utils/common'
import FloatingInput from '@/utils/form/FloatingInput.vue';
import { calculateLoanPayment } from '@/utils/pmt';
import { useToast } from "vue-toastification";

const route = useRoute();
const personalDetail = ref();
const toast = useToast();

const modelParameter = ref<any>({
   id: 0,
   customerId: '',
   amountRequired: 2100,
   term: 0,
   title: '',
   firstName: '',
   lastName: '',
   fullname: '',
   email: '',
   mobile: '',
   dateOfBirth: '',
   repaymentAmount: 0,
   establishmentFee: 0,
   totalInterest: 0,
   repaymentsFrom: 56.15
})

axios.get(`/api/loan/${route.params.id}`).then((response) => {
  personalDetail.value = response.data.data
  modelParameter.value.id = personalDetail.value.id
  modelParameter.value.customerId = personalDetail.value.customerId
  modelParameter.value.fullname = `${personalDetail.value.firstName} ${personalDetail.value.lastName}`
  modelParameter.value.amountRequired = personalDetail.value.amountRequired
  modelParameter.value.term = personalDetail.value.term
  modelParameter.value.title = personalDetail.value.title
  modelParameter.value.firstName = personalDetail.value.firstName
  modelParameter.value.lastName = personalDetail.value.lastName
  modelParameter.value.email = personalDetail.value.email
  modelParameter.value.mobile = personalDetail.value.mobile
  modelParameter.value.dateOfBirth = dateFormat(personalDetail.value.dateOfBirth)
});

const dollarFormat = (sliderVal: number) => `$${('' + sliderVal).replace(/\B(?=(\d{3})+(?!\d))/g, ',')}`

const dateOfBirth = ref('')
const applyMask = (event: InputEvent) : void => {
      const input = event.target as HTMLInputElement;
      let value = input.value.replace(/\D/g, '');

      if (value.length > 2) {
        value = `${value.substring(0, 2)}/${value.substring(2)}`;
      }
      if (value.length > 5) {
        value = `${value.substring(0, 5)}/${value.substring(5, 9)}`;
      }

      input.value = value;
      dateOfBirth.value = value;
}

const isCalculateClicked = ref(false)
const isRedirectPage = ref(false)
const calculateQuoute = (principal: number, repaymentF: number, terminYears: number) => {
  const payment = calculateLoanPayment(principal, repaymentF, terminYears);
  modelParameter.value.repaymentAmount =  payment.totalRepayment
  modelParameter.value.totalInterest = payment.totalInterest
  modelParameter.value.establishmentFee = payment.establishmentFee
  isCalculateClicked.value = true
  isRedirectPage.value = true;
}

const isEditInformation = ref(false)

const editInformation = () => {
  isEditInformation.value = !isEditInformation.value
}

const toMonths = (years: number) => {
   return `${years * 12} months`
}

const underApplyNow = (repayment: number, establishmentFee: number, totalInterest: number ) => {
    return `Total repayments $${repayment.toFixed(2)}, madeup of an establishedment fee of ${establishmentFee.toFixed(2)}, interest of
            ${totalInterest.toFixed(2)}. The repayment amount is based on the variables selected, is subject to our assesment and suitability,
            and other important terms and conditions apply.`
} 

const isEditFinancialDetails = ref(false);
const editFinancialDetails = () => {
  isEditFinancialDetails.value = !isEditFinancialDetails.value
  
  const payment = calculateLoanPayment(modelParameter.value.amountRequired, modelParameter.value.repaymentsFrom, modelParameter.value.term);
  modelParameter.value.repaymentAmount =  payment.totalRepayment
  modelParameter.value.totalInterest = payment.totalInterest
  modelParameter.value.establishmentFee = payment.establishmentFee
}

const isPreApproved = ref(false);
const applyNow = () => {
  axios.post("/api/loan/apply", modelParameter.value)
    .then(response => {
      isPreApproved.value = true;
      isCalculateClicked.value = false;
      isRedirectPage.value = true;
    })
    .catch(error => {
      toast.error(error.response.data.exceptiondetails.messages[0], {
        timeout: 2000
      });
      console.log(error)
    });
}
</script>
<template>
 <div class="position-absolute top-50 start-50 translate-middle col-md-6">
  <div class="card py-3 px-3 animate fadeIn animate__delay" v-if="!isRedirectPage">    
    <div class="card-body">
     <div class="text-center">
          <p class="fw-bold fs-2 card-title">Quote calculator</p>
     </div>

     <div class="pt-5">
      <vue-slider 
         :dot-size="20"
         :height="15"
         :min="2100"
         :max="50000"
         :contained="true"
         :drag-on-click="true"
         :tooltip-formatter="dollarFormat"
         :tooltip-style="{background: '#00d1cf'}"
         :process-style="{background: '#00d1cf'}"
         tooltip="always"
         v-model="modelParameter.amountRequired"
      />
      <div class="d-flex justify-content-between px-3">
          <p class="custom_slider-label">$2100</p>
          <p class="custom_slider-label">How much do you need?</p>
          <p class="custom_slider-label">$50000</p>
      </div>
     </div>

     <div class="row px-2">
       <div class="col-md-4">
        <div class="form-group">
            <label for="selectOption" class="selectLabel">Title</label>
              <select class="form-control form-select" id="selectOption" v-model="modelParameter.title">
                <option value="Mr.">Mr.</option>
                <option value="Ms.">Ms.</option>
                <option value="Mrs.">Mrs.</option>
                <option value="Miss">Miss</option>
              </select>
            </div>
       </div>
       <div class="col-md-4">
         <div class="form-group pt-1">
           <FloatingInput 
                id="firstName" 
                v-model="modelParameter.firstName" 
                :value="modelParameter.firstName"  
                type="text" 
                placeholder="First name" 
                label="First name" 
                :validation="'required'"
                errorMessage="Please enter a first name" 
            />
         </div>        
       </div>
       <div class="col-md-4">
         <div class="form-group pt-1">
           <FloatingInput 
                id="lastName" 
                v-model="modelParameter.lastName" 
                :value="modelParameter.lastName"  
                type="text" 
                placeholder="Last name"
                label="Last name"
                :validation="'required'"
                errorMessage="Please enter a last name" 
            />
         </div>        
       </div>
     </div>

     <div class="row px-2">
      <div class="col-md-12">
         <div class="form-group pt-1">
          <FloatingInput 
                id="email" 
                v-model="modelParameter.email" 
                :value="modelParameter.email"  
                type="email" 
                placeholder="Your email"
                label="Email"
                :validation="'required'"
                errorMessage="Please enter your email" 
            />
         </div>        
       </div>
     </div>

     <div class="row px-2">
       <div class="col-md-6">
         <div class="form-group pt-1">
          <FloatingInput 
                id="mobile" 
                v-model="modelParameter.mobile" 
                :value="modelParameter.mobile"  
                type="text" 
                placeholder="Mobile number"
                label="Mobile"
                :validation="'required'"
                errorMessage="Please enter your mobile number" 
          />
         </div>        
       </div>
       <div class="col-md-6">
         <div class="form-group pt-1">
            <FloatingInput 
                id="dateOfBirth" 
                v-model="modelParameter.dateOfBirth" 
                value="modelParameter.dateOfBirth"  
                type="text" 
                placeholder="Date of Birth (MM/DD/YYYY)"
                label="Date of Birth (MM/DD/YYYY)"
                :validation="'required'"
                errorMessage="Please enter your date of birth" 
                @input="applyMask"
                maxlength="10"
          />
         </div>        
       </div>
     </div>
      <div class="text-center pt-5">
        <a class="btn btn-primary btn-lg py-3 px-5" @click="calculateQuoute(modelParameter.amountRequired, modelParameter.repaymentsFrom, modelParameter.term)">
          Calculate quote
        </a>
      </div>
      <div class="text-center text-muted pt-4">
        <p class="custom_p">Quote does not affect your credit score.</p>
      </div>
    </div>
    </div>

<!-- CALCULATE -->
    <div class="card py-3 px-5 animate fadeIn animate__delay" v-if="isCalculateClicked">    
      <div class="card-body">
        <div class="text-center">
             <p class="fw-bold fs-2 card-title">Your quote</p>
        </div>

        <div class="d-flex justify-content-between">
          <p class="fw-bold fs-5 card-title">Your Information</p>
          <p class="selectLabel fs-5" @click="editInformation">{{ isEditInformation ? "Save" : "Edit"}}</p>
        </div>

        <div class="d-flex justify-content-between" v-if="!isEditInformation">
          <p class="fw-bold fs-6 card-title">Name</p>
          <p class="fw-bold fs-6 card-title">{{modelParameter.firstName}} {{modelParameter.lastName}}</p>
        </div>
        <div class="row animate fadeIn animate__delay" v-if="isEditInformation">
          <div class="col-md-6" >
            <FloatingInput 
                id="firstName" 
                v-model="modelParameter.firstName" 
                :value="modelParameter.firstName"  
                type="text" 
                placeholder="First name" 
                label="First name" 
                :validation="'required'"
                errorMessage="Please enter a first name" 
            />
           </div>  
           <div class="col-md-6">
              <FloatingInput 
                  id="lastName" 
                  v-model="modelParameter.lastName" 
                  :value="modelParameter.lastName"  
                  type="text" 
                  placeholder="Last name"
                  label="Last name"
                  :validation="'required'"
                  errorMessage="Please enter a last name" 
              />
           </div>
        </div>

        <div class="d-flex justify-content-between" v-if="!isEditInformation">
          <p class="fw-bold fs-6 card-title">Mobile</p>
          <p class="fw-bold fs-6 card-title" v-if="!isEditInformation">{{modelParameter.mobile}}</p>

        </div>
        <FloatingInput 
                id="mobile" 
                v-model="modelParameter.mobile" 
                :value="modelParameter.mobile"  
                type="text" 
                placeholder="Mobile number"
                label="Mobile"
                :validation="'required'"
                errorMessage="Please enter your mobile number" 
                v-if="isEditInformation"
                class="animate fadeIn animate__delay"
        />

        <div class="d-flex justify-content-between" v-if="!isEditInformation" >
          <p class="fw-bold fs-6 card-title">Email</p>
          <p class="fw-bold fs-6 card-title">{{modelParameter.email}}</p>
        
        </div>
        <FloatingInput 
                id="email" 
                v-model="modelParameter.email" 
                :value="modelParameter.email"  
                type="email" 
                placeholder="Your email"
                label="Email"
                :validation="'required'"
                errorMessage="Please enter your email" 
                v-if="isEditInformation"
                class="animate fadeIn animate__delay"
        />

        <div class="d-flex justify-content-between pt-4">
          <p class="fw-bold fs-5 card-title">Finance details</p>
          <p class="selectLabel fs-5" @click="editFinancialDetails">{{ isEditFinancialDetails ? "Save" : "Edit"}}</p>
        </div>

        <div v-if="!isEditFinancialDetails">
          <div class="d-flex justify-content-between">
            <p class="fw-light fs-5 finance-details m-0">Finance amount</p>
            <p class="fw-bold fs-5 selectLabel m-0 ">${{ modelParameter.amountRequired }}</p>
          </div>

          <div class="d-flex justify-content-between">
            <p class="underscore"></p>
            <p class="fw-bold finance-sub-details">over {{ toMonths(modelParameter.term) }}</p>
          </div>
        </div>

      <div v-if="isEditFinancialDetails"
          class="animate fadeIn animate__delay">
          <vue-slider 
             :dot-size="20"
             :height="15"
             :min="2100"
             :max="50000"
             :contained="true"
             :drag-on-click="true"
             :tooltip-formatter="dollarFormat"
             :tooltip-style="{background: '#00d1cf'}"
             :process-style="{background: '#00d1cf'}"
             tooltip="always"
             v-model="modelParameter.amountRequired"
          />
          <div class="d-flex justify-content-between px-3">
              <p class="custom_slider-label">$2100</p>
              <p class="custom_slider-label">How much do you need?</p>
              <p class="custom_slider-label">$50000</p>
          </div>
        </div>

        <FloatingInput 
                id="amountRequired" 
                v-model="modelParameter.term" 
                :value="modelParameter.term"  
                type="text" 
                placeholder="Term (In Years)"
                label="Term (In Years)"
                :validation="'required'"
                errorMessage="Please enter term" 
                v-if="isEditFinancialDetails"
                class="animate fadeIn animate__delay"
        />


        <div class="pt-3" v-if="!isEditFinancialDetails">
          <div class="d-flex justify-content-between">
            <p class="fw-light fs-5 finance-details m-0">Repayments from</p>
            <p class="fw-bold fs-5 selectLabel m-0 ">${{ modelParameter.repaymentsFrom }}</p>
          </div>

          <div class="d-flex justify-content-between">
            <p class="underscore p-0"></p>
            <p class="fw-bold finance-sub-details">Weekly</p>
          </div>
        </div>

        <FloatingInput 
                id="repaymentsFrom" 
                v-model="modelParameter.repaymentsFrom" 
                :value="modelParameter.repaymentsFrom"  
                type="text" 
                placeholder="Repayment From (Weekly)"
                label="Repayment From (Weekly)"
                :validation="'required'"
                errorMessage="Please enter amount" 
                v-if="isEditFinancialDetails"
                class="animate fadeIn animate__delay"
        />
         
        <div class="text-center pt-5">
          <a class="btn btn-primary btn-lg py-3 px-5" @click="applyNow">
            Apply now
          </a>
        </div>
        <div class="text-center text-muted pt-4">
         <p class="custom_p_applyNow">{{underApplyNow(modelParameter.repaymentAmount, modelParameter.establishmentFee, modelParameter.totalInterest)}}</p>
        </div>
    </div>
  </div>

  <!-- CALCULATE -->
  <div class="card py-3 px-5 animate fadeIn animate__delay" v-if="isPreApproved">    
      <div class="card-body">
        <div class="text-center">
             <p class="fw-bold fs-2 card-title">Applied</p>
        </div>
        <div class="text-center"> 
            <p class="fw-normal fs-6">
              You're loan has been pre-approved. We will contact you for with more information soon.
            </p>
        </div>
        <div class="text-center pt-5">
          <a class="btn btn-primary btn-lg py-3 px-5">
            Thank you!
          </a>
        </div>
      </div>
  </div>
 </div>
</template>
<style scoped>
.card {
   border-radius: 40px; 
   overflow: hidden;
   border: 0;
   box-shadow: 0 2px 20px rgba(0, 0, 0, 0.06),
               0 2px 4px rgba(0, 0, 0, 0.07);
   transition: all 0.15s ease;
}

.card:hover {
   box-shadow: 0 6px 30px rgba(0, 0, 0, 0.1),
               0 10px 8px rgba(0, 0, 0, 0.015);
}

.card-body .card-title {
  font-family: 'Lato', sans-serif;
  font-weight: 700;
  letter-spacing: 0.3px;
  font-size: 24px;
  color: #777777;
}

.finance-details{
  font-family: 'Lato', sans-serif;
  letter-spacing: 0.3px;
  color: #777777;
  font-weight: 100;
}
.finance-sub-details{
  color: #777777;
}
.underscore{
  margin-top: 0;
  border-bottom: 1px dotted;
  width: 70%;
}

.card-text {
  font-family: 'Lato', sans-serif;
  font-weight: 400;
  font-size: 15px;
  letter-spacing: 0.3px;
  color: #4E4E4E;
  
}

.card .container {
  width: 100%;
  background: #F0EEF8;
  border-radius: 30px;
  height: auto;
  display: flex;
  align-items: center;
  justify-content: center;
}

.btn {
  background: #4cd133;
  border: 0;
  border-radius: 0;
  width: 350px;
  box-shadow: 0px 12px 10px rgba(0, 0, 0, 0.3);
  font-weight: 500;
  transition: all 0.2s ease;
  letter-spacing: 0.3px;
}

.btn:hover {
  background: #441CFF;
}

.btn:focus {
  background: #441CFF;
  outline: 0;  
}

.custom_slider-label, .custom_p{
  color:#acacac;
  font-size: smaller;
  font-weight: bold;
}

.custom_p_applyNow{
  color:#acacac;
  font-size: 12px;
  font-weight: normal;
}

select.form-control {
  border-top: none;
  border-left: none;
  border-right: none;
  border-radius: 0;
  border-bottom: 1px solid #b6b6b6;
}

select.form-control:focus {
  box-shadow: none;
}
.selectLabel{
  color: #00d1cf;
  font-weight: normal;
}

@-webkit-keyframes fadeIn {
	from {
		opacity: 0;
	}
	to {
		opacity: 1;
	}
}
@keyframes fadeIn {
	from {
		opacity: 0;
	}
	to {
		opacity: 1;
	}
}
.animate {
	-webkit-animation-duration: .2s;
	animation-duration: .2s;
	-webkit-animation-fill-mode: both;
	animation-fill-mode: both;
}
.animate__delay {
	-webkit-animation-delay: .2s;
	-moz-animation-delay: .2s;
	animation-delay: .2s;
}
.fadeIn {
	-webkit-animation-name: fadeIn;
	animation-name: fadeIn;
}
</style>